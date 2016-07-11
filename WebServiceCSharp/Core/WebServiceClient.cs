﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;
namespace WebServiceCSharp.Core
{
   public class WebServiceClient
    {
        //WebSerive Client Properties
        public HttpWebRequest Request { get; set; }
        public HttpWebResponse Response { get; private set; }
        public String EndPointURI { get; set; }
        public String ResponseBody { get; private set; }
        private String[] _serviceRequestValues;
        public long TimeTaken;

        //Excel values
        public String _URISegment { get; set; }
        public String _urlParameters { get; set; }
        public String _requestMethod { get; set; }
        public String _requestHeaders { get; set; }
        public String _requestBody { get; set; }
        public String _expectedResponseBody { get; set; }

        public WebServiceClient(String endPointURI="",String dataIdentifier=null)
        {
            EndPointURI = endPointURI;
            _requestBody = "";
            _requestHeaders = "";
            _requestMethod = "GET";
            _urlParameters = "";
            _URISegment = "";
            _expectedResponseBody = "";
            if(dataIdentifier!=null){
                _serviceRequestValues = Utils.GetDataFromExcel(dataIdentifier).First();
                LoadServiceRequestValues();
            }
           
        }

        public WebServiceClient(String endPointURI, String[] values)
        {
            EndPointURI = endPointURI;
            _requestBody = "";
            _requestHeaders = "";
            _requestMethod = "GET";
            _urlParameters = "";
            _URISegment = "";
            _expectedResponseBody = "";
            _serviceRequestValues = values;
            LoadServiceRequestValues();
        }

       /// <summary>
       /// Loads the Request values from the _serviceRequestValues to the corresponding properties
       /// </summary>
        private void LoadServiceRequestValues()
        {
            _URISegment = (_serviceRequestValues.Length > 0 && _serviceRequestValues[0] != null ? _serviceRequestValues[0] : "");

            //Setting the request method
            if (_serviceRequestValues.Length > 1)
                _requestMethod = String.IsNullOrEmpty(_serviceRequestValues[1]) ? "GET" : _serviceRequestValues[1];

            //Setting the Request header
            if (_serviceRequestValues.Length > 2 && !String.IsNullOrEmpty(_serviceRequestValues[2]))
                _requestHeaders = _serviceRequestValues[2];

            //Setting Request Body
            if (_serviceRequestValues.Length > 3 && !String.IsNullOrEmpty(_serviceRequestValues[3]))
                _requestBody = _serviceRequestValues[3];
        }


        /// <summary>
        /// Set the Request headers and request body from the data got from data identifier
        /// </summary>
        /// <returns></returns>
        public WebServiceClient SetRequest()
        {
            if (_serviceRequestValues == null)
            {
                Request = (HttpWebRequest)WebRequest.Create(EndPointURI);
                Logger.Debug("Set EndPoint as : {0}", Request.RequestUri);

                if (Config.IsConfigValuePresent("DefaultHeaders"))
                    SetRequestHeaders(Config.GetConfigValue("DefaultHeaders"));
                return this;
            }

            EndPointURI = EndPointURI + _URISegment + _urlParameters;
            
            Request = (HttpWebRequest)WebRequest.Create(EndPointURI);
            Logger.Debug("Set EndPoint as : {0}", Request.RequestUri);

            if (Config.IsConfigValuePresent("DefaultHeaders"))
                SetRequestHeaders(Config.GetConfigValue("DefaultHeaders"));

            Request.Method = _requestMethod;
            Logger.Debug("Request method :{0}", Request.Method);

            if (!String.IsNullOrEmpty(_requestHeaders))
                SetRequestHeaders(_requestHeaders);

            if (!String.IsNullOrEmpty(_requestBody))
                SetRequestBody(_requestBody);

            Logger.Debug(Request);

            return this;
        }

        /// <summary>
        /// Set the Request body 
        /// </summary>
        /// <param name="body">The Request body, or the path of the file(.txt,.json,.xml)</param>
        /// <returns></returns>
        public WebServiceClient SetRequestBody(String body)
        {
            String parameterBody;
            if (body.EndsWith(".json"))
            {
                if (String.IsNullOrEmpty(Request.ContentType))
                    Request.ContentType = "application/json";
                parameterBody = Utils.GetFileAsString(body);
            }
            else if (body.EndsWith(".xml"))
            {
                if (String.IsNullOrEmpty(Request.ContentType))
                    Request.ContentType = "text/xml";
                parameterBody = Utils.GetFileAsString(body);
            }
            else if (body.EndsWith(".txt"))
            {
                parameterBody = Utils.GetFileAsString(body);
            }
            else
                parameterBody = body;

            Request.ContentLength = parameterBody.Length;
            StreamWriter requestWriter = new StreamWriter(Request.GetRequestStream());
            requestWriter.AutoFlush = true;
            requestWriter.Write(parameterBody);

            Logger.Debug("Set Request Body: " + parameterBody);
            return this;
        }

        /// <summary>
        /// Set Header values 
        /// </summary>
        /// <param name="headerString">'\n' or ',' delimited header and value pairs (seperated by ':')
        /// eg. header1Name : header1Value,header2Name : header2Value
        /// </param>
        public WebServiceClient SetRequestHeaders(String headerString)
        {
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
                Logger.Debug("Set Header {0}={1} ", headerName, headerValue);
            }
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
                throw e;
            }

            try
            {
                using (StreamReader responseReader = new StreamReader(Response.GetResponseStream()))
                {
                    ResponseBody = responseReader.ReadToEnd();
                    responseReader.Close();
                    Logger.Debug("Response body : \n {0}", ResponseBody);
                }

            }
            catch (Exception e)
            {
                Logger.Error("Error reading the response", e);
                throw e;
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
                return Utils.DeserializeJSON(GetResponseBody(), type);

            else if (Response.ContentType.Contains("xml"))
            {
                return Utils.DeserializeXML(GetResponseBody(), type);
            }
            return null;
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
                throw e;
            }
           
        }

       /// <summary>
       /// Set the URL parameters
       /// </summary>
       /// <param name="paramValuePairs">key value pairs of URL parameters to be appended to the endpoint</param>
        public void SetURLParameters(Dictionary<String, String> paramValuePairs)
        {

            int index = 0;
            foreach (String key in paramValuePairs.Keys)
            {
                _urlParameters = String.Format("{0}{1}={2}", String.IsNullOrWhiteSpace(_urlParameters)?"?":"&", _urlParameters , key);  
            }
            Logger.Debug("Constructed URL parameter : {0}", _urlParameters);
        }
    }
}