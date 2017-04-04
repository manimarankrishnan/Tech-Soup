using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Xml;
using Utils.Core;
using System.Web;
namespace WebServiceCSharp.Core
{
    public class WebServiceClient
    {
        //WebSerive Client Properties
        public HttpWebRequest Request { get; set; }
        public HttpWebResponse Response { get; private set; }
        public String EndPointURI { get; set; }
        public String ResponseBody { get; private set; }
        private Data _requestData;
        public long TimeTaken;

        //Excel values
        public String URISegment { get; set; }
        public String UrlParameters { get; set; }
        public String RequestMethod { get; set; }
        public String RequestHeaders { get; set; }
        public String RequestBody { get; set; }
        public String ExpectedResponseBody { get; set; }

        public WebServiceClient(String endPointURI = "", String dataIdentifier = null)
        {
            EndPointURI = endPointURI;
            RequestBody = "";
            RequestHeaders = "";
            RequestMethod = "GET";
            UrlParameters = "";
            URISegment = "";
            ExpectedResponseBody = "";
            if (dataIdentifier != null)
            {
                _requestData = new Data(dataIdentifier);
                LoadServiceRequestValues();
            }

        }

        public WebServiceClient(String endPointURI, Data values)
        {
            EndPointURI = endPointURI;
            RequestBody = "";
            RequestHeaders = "";
            RequestMethod = "GET";
            UrlParameters = "";
            URISegment = "";
            ExpectedResponseBody = "";
            _requestData = values;
            LoadServiceRequestValues();
        }

        /// <summary>
        /// Loads the Request values from the _serviceRequestValues to the corresponding properties
        /// </summary>
        private void LoadServiceRequestValues()
        {
            String URISegmentKey = "URISegment";
            String MethodKey = "Method";
            String HeadersKey = "Headers";
            String RequestBodyKey = "PostBody";
            String ExpectedResponseBodyKey = "Expected Response Body";

            String value;
            if(_requestData.TryGetValue(URISegmentKey,out value) &&  !String.IsNullOrEmpty(value))
                URISegment =value;
            if (_requestData.TryGetValue(MethodKey, out value) && !String.IsNullOrEmpty(value))
                RequestMethod = value;
            if (_requestData.TryGetValue(HeadersKey, out value) && !String.IsNullOrEmpty(value))
                RequestHeaders = value;
            if (_requestData.TryGetValue(RequestBodyKey, out value) && !String.IsNullOrEmpty(value))
                RequestBody = value;
            if (_requestData.TryGetValue(ExpectedResponseBodyKey, out value) && !String.IsNullOrEmpty(value))
                ExpectedResponseBody = value;
           

        }

        private void loadRequestBody(String body)
        {

            String parameterBody;
            if (body.EndsWith(".json"))
            {
                if (String.IsNullOrEmpty(Request.ContentType))
                    Request.ContentType = "application/json";
                parameterBody = GeneralUtils.GetFileAsString(body);
            }
            else if (body.EndsWith(".xml"))
            {
                if (String.IsNullOrEmpty(Request.ContentType))
                    Request.ContentType = "text/xml";
                parameterBody = GeneralUtils.GetFileAsString(body);
            }
            else if (body.EndsWith(".txt"))
            {
                parameterBody = GeneralUtils.GetFileAsString(body);
            }
            else
            {
                parameterBody = body;
            }

            if (parameterBody.Contains("<?xml"))
                Request.ContentType = "text/xml";

            Request.ContentLength = parameterBody.Length;
            StreamWriter requestWriter = new StreamWriter(Request.GetRequestStream());
            requestWriter.AutoFlush = true;
            requestWriter.Write(parameterBody);

            Logger.Debug("Set Request Body: " + parameterBody);

        }

        /// <summary>
        /// Set the Request body 
        /// </summary>
        /// <param name="body">The Request body, or the path of the file(.txt,.json,.xml)</param>
        /// <returns></returns>
        public WebServiceClient SetRequestBody(String body)
        {
            if (Request != null)
            {
                Exception e = new Exception("Call the SetRequestBody method before calling SetRequest method.");
                Logger.Error(e);
                throw e;
            }
            this.RequestBody = body;
            return this;
        }

        /// <summary>
        /// Set Header values 
        /// </summary>
        /// <param name="headerString">'\n' or ',' delimited header and value pairs (seperated by ':')
        /// eg. header1Name : header1Value,header2Name : header2Value
        /// </param>
        private WebServiceClient SetRequestHeaders(String headerString)
        {
            if (Request == null)
            {
                Exception e = new Exception("Call the SetRequest method before calling SetRequestHeaders method.");
                Logger.Error(e);
                throw e;
            }

            String[] headers = headerString.Split(new Char[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (String header in headers)
            {
                int indexOfColon = header.IndexOf(':');
                String headerName = header.Substring(0, indexOfColon);
                String headerValue = header.Substring(indexOfColon + 1);
                switch (headerName.Trim().ToLower())
                {
                    case "content-type":
                        Request.ContentType = headerValue.Trim();
                        break;
                    case "accept":
                        Request.Accept = headerValue.Trim();
                        break;
                    default:
                        Request.Headers.Add(headerName.Trim(), headerValue.Trim());
                        break;
                }
                //Logger.Debug("Set Header {0}={1} ", headerName, headerValue);
            }

            foreach (String key in Request.Headers.Keys)
            {
                Logger.Debug("Set Header {0}={1} ", key, Request.Headers[key]);
            }

            return this;
        }


        /// <summary>
        /// Add the URL parameters
        /// </summary>
        /// <param name="paramValuePairs">key value pairs of URL parameters to be appended to the endpoint</param>
        public WebServiceClient AddURLParametersToURL(Dictionary<String, String> paramValuePairs)
        {
            if (Request != null)
            {
                Exception e = new Exception("Call AddURLParametersToURL method before calling the SetRequestMethod");
                Logger.Error(e);
                throw e;
            }
            foreach (String key in paramValuePairs.Keys)
            {
                AddURLParametersToURL(String.Format("{0}={1}", paramValuePairs[key], key));
            }
            Logger.Debug("Constructed URL parameter : {0}", UrlParameters);
            return this;
        }

        /// <summary>
        ///  Add URL parameters to the endpoint url
        /// </summary>
        /// <param name="keyValue">URL parameter in format &lt;key&gt;=&lt;value&gt;</param>
        /// <returns>WebServiceClient</returns>
        public WebServiceClient AddURLParametersToURL(String keyValue)
        {
            if (Request != null)
            {
                Exception e = new Exception("Call AddURLParametersToURL method before calling the SetRequestMethod");
                Logger.Error(e);
                throw e;
            }
            if (String.IsNullOrEmpty(UrlParameters))
                UrlParameters = "?";
            UrlParameters = UrlParameters + (UrlParameters.Equals("?") ? "" : "&") + keyValue;
            Logger.Debug("Constructed URL parameter : {0}", UrlParameters);
            return this;
        }

        /// <summary>
        /// Adds the headers to the list of headers to be included in the Request
        /// </summary>
        /// <param name="headerString">'\n' or ',' delimited header and value pairs (seperated by ':')
        /// eg. header1Name : header1Value,header2Name : header2Value
        /// </param>
        /// <returns>WebServiceClient</returns>
        public WebServiceClient AddHeaders(String headerString)
        {
            if (Request != null)
            {
                Exception e = new Exception("Call AddHeaders method before calling the SetRequestMethod");
                Logger.Error(e);
                throw e;
            }
            RequestHeaders = RequestHeaders + "," + headerString;
            Logger.Info("_requestHeaders = " + RequestHeaders);
            return this;
        }

        /// <summary>
        /// Adds the headers to the list of headers to be included in the Request 
        /// </summary>
        /// <param name="headers"></param>
        /// <returns>WebServiceClient</returns>
        public WebServiceClient AddHeaders(Dictionary<String, String> headers)
        {
            if (Request != null)
            {
                Exception e = new Exception("Call AddHeaders method before calling the SetRequestMethod");
                Logger.Error(e);
                throw e;
            }

            foreach (string key in headers.Keys)
            {
                RequestHeaders = String.Format("{0},{1}:{2}", RequestHeaders, key, headers[key]);
            }

            RequestHeaders = RequestHeaders + "," + headers;
            Logger.Info("_requestHeaders = " + RequestHeaders);
            return this;
        }


        /// <summary>
        /// Set the Request headers and request body from the data got from data identifier
        /// </summary>
        /// <returns></returns>
        public WebServiceClient SetRequest()
        {
            //if (_requestData == null)
            //{
            //    Request = (HttpWebRequest)WebRequest.Create(EndPointURI);
            //    Logger.Debug("Set EndPoint as : {0}", Request.RequestUri);

            //    if (Config.IsConfigValuePresent("DefaultHeaders"))
            //        SetRequestHeaders(Config.GetConfigValue("DefaultHeaders"));
            //    return this;
            //}

            EndPointURI = EndPointURI + URISegment + UrlParameters;

            Request = (HttpWebRequest)WebRequest.Create(EndPointURI);
            Logger.Debug("Set EndPoint as : {0}", Request.RequestUri);

            if (Config.IsConfigValuePresent("DefaultHeaders"))
                SetRequestHeaders(Config.GetConfigValue("DefaultHeaders"));

            Request.Method = RequestMethod;
            Logger.Debug("Request method :{0}", Request.Method);

            if (!String.IsNullOrEmpty(RequestHeaders))
                SetRequestHeaders(RequestHeaders);

            if (!String.IsNullOrEmpty(RequestBody))
                loadRequestBody(RequestBody);

            Logger.Debug(Request);

            return this;
        }

        /// <summary>
        /// Call the Service and set the Response
        /// </summary>
        /// <returns></returns>
        public WebServiceClient CallService()
        {
            if (Request == null)
            {
                Exception e = new Exception("Call the SetRequest method before calling the service");
                Logger.Error(e);
                throw e;
            }

            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                Response = (HttpWebResponse)Request.GetResponse();
                stopWatch.Stop();
                TimeTaken = stopWatch.ElapsedMilliseconds;
                Logger.Debug("Status Code:{0}", Response.StatusCode);
                Logger.Debug("Time take for the service response : " + TimeTaken + " milliseconds");
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw ;
            }

            try
            {
                using (StreamReader responseReader = new StreamReader(Response.GetResponseStream()))
                {
                    ResponseBody = responseReader.ReadToEnd();
                    //responseReader.Close();
                    Logger.Debug("Response body : \n {0}", ResponseBody);
                }

            }
            catch (Exception e)
            {
                Logger.Error("Error reading the response", e);
                throw ;
            }
            return this;
        }

        /// <summary>
        /// Calls the Service and returns the response body
        /// </summary>
        /// <returns></returns>
        public String GetResponseBody()
        {
            if (Response == null)
            {
                Exception e = new Exception("Call the method CallService before getting the response body");
                Logger.Error(e);
                throw e;
            }
            return ResponseBody;
        }

        /// <summary>
        /// Get the response body as an object 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Object GetResponseAsObject(Type type)
        {
            if (Response == null)
            {
                Exception e = new Exception("Call the CallService method before getting the response");
                Logger.Error(e);
                throw e;
            }

            if (Response.ContentType.Contains("json"))
                return GeneralUtils.DeserializeJSON(GetResponseBody(), type);

            else if (Response.ContentType.Contains("xml"))
            {
                return GeneralUtils.DeserializeXML(GetResponseBody(), type);
            }
            return null;
        }


        /// <summary>
        /// Get the Response Body as an XMLDocument 
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetResponseAsXMLDocument()
        {
            if (Response == null)
            {
                Exception e = new Exception("Call the CallService method before getting the response");
                Logger.Error(e);
                throw e;
            }

            if (Response.ContentType.Contains("xml"))
            {
                Logger.Debug("Conten-Type of the Response(" + Response.ContentType + ") is not xml");
            }
            try
            {
                XmlDocument result = new XmlDocument();
                result.LoadXml(GetResponseBody().Trim());
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw ;
            }

        }


        /// <summary>
        /// Call the SetRequest(),CallService(),GetResponseAsObject() methods consecutively and return response as an object.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Object CallServiceGetResponseAsObject(Type type)
        {
            try
            {
                return SetRequest().CallService().GetResponseAsObject(type);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw ;
            }

        }


        /// <summary>
        /// return status code of the response. 
        /// </summary>
        /// <returns></returns>
        public String GetStatusCodeOfResponse()
        {
            try
            {
                if (Response == null)
                {
                    Exception e = new Exception("Call the CallService method before getting the response");
                    Logger.Error(e);
                    throw e;
                }
                else
                    return Response.StatusCode.ToString();

            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw ;
            }
        }


    }
}
