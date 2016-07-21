using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System.Collections.ObjectModel;
using WebServiceCSharp.Core;
using System.Drawing;
using OpenQA.Selenium.Interactions.Internal;

namespace SeleniumCSharp.Selenium
{

    public class WebElementWrapper : IWebElement, ISearchContext, ILocatable, IWrapsElement
    {
        /*
        private const double DefaultTimeout = 90.0;
        public By By { get; private set; }

        private IWebElement element { get; set; }

        public IWebElement WrappedElement
        {
            get
            {
                return this.element;
            }
            set
            {
                this.element = value;
            }
        }

        public ReadOnlyCollection<IWebElement> WrappedElements { get; private set; }

        public bool Displayed
        {
            get
            {
                this.InitializeWebElement();
                bool displayed;
                try
                {
                    displayed = this.element.Displayed;
                }
                catch (WebDriverException ex)
                {
                   Logger.Error("Attempted to get the displayed attribute of this element by: '" + this.By.ToString() + "', but failed.");
                    throw new Exception("Attempted to get the displayed attribute of this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
                }
                Logger.Info("Got the displayed attribute: '" + (object)displayed + "' of this element by: '" + this.By.ToString() + "'.");
                return displayed;
            }
            private set
            {
            }
        }

        public bool Enabled
        {
            get
            {
                this.InitializeWebElement();
                bool enabled;
                try
                {
                    enabled = this.element.Enabled;
                }
                catch (WebDriverException ex)
                {
                    Logger.Error("Attempted to get the enabled attribute of this element by: '" + this.By.ToString() + "', but failed.");
                    throw new Exception("Attempted to get the enabled attribute of this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
                }
                Logger.Info("Got the enabled attribute: '" + (object)enabled + "' of this element by: '" + this.By.ToString() + "'.");
                return enabled;
            }
            set
            {
                
            }
        }

        public Point Location
        {
            get
            {
                this.InitializeWebElement();
                Point location;
                try
                {
                    location = this.element.Location;
                }
                catch (WebDriverException ex)
                {
                    Logger.Error("Attempted to get the location of this element by: '" + this.By.ToString() + "', but failed.");
                    throw new Exception("Attempted to get the location of this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
                }
                Logger.Info("Got the location: '" + (object)location + "' of this element by: '" + this.By.ToString() + "'.");
                return location;
            }
            private set
            {
            }
        }

        public bool Selected
        {
            get
            {
                this.InitializeWebElement();
                bool selected;
                try
                {
                    selected = this.element.Selected;
                }
                catch (WebDriverException ex)
                {
                    Logger.Error("Attempted to get the selected attribute of this element by: '" + this.By.ToString() + "', but failed.");
                    throw new Exception("Attempted to get the selected attribute of this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
                }
                Logger.Info("Got the selected attribute: '" + (object)selected + "' of this element by: '" + this.By.ToString() + "'.");
                return selected;
            }
           private set
            {
              
            }
        }

        public Size Size
        {
            get
            {
                this.InitializeWebElement();
                Size size;
                try
                {
                    size = this.element.Size;
                }
                catch (WebDriverException ex)
                {
                    Logger.Error("Attempted to get the size of this element by: '" + this.By.ToString() + "', but failed.");
                    throw new Exception("Attempted to get the size of this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
                }
                Logger.Info("Got the size: '" + (object)size + "' of this element by: '" + this.By.ToString() + "'.");
                return size;
            }
            private set
            {
            }
        }

        public string TagName
        {
            get
            {
                this.InitializeWebElement();
                string tagName;
                try
                {
                    tagName = this.element.TagName;
                }
                catch (WebDriverException ex)
                {
                    this.Report.Write("Attempted to get the tag name of this element by: '" + this.By.ToString() + "', but failed.");
                    throw new Exception("Attempted to get the tag name of this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
                }
                this.Report.Write("Got the tag name: '" + tagName + "' of this element by: '" + this.By.ToString() + "'.");
                return tagName;
            }
            set
            {
                this.InitializeWebElement();
                if (this.element.GetType() == typeof(DummyWebElement))
                {
                    ((DummyWebElement)this.element).TagName = value;
                }
                else
                {
                    if (!(this.element.GetType() == typeof(NgWebElement)) || !(((NgWebElement)this.element).WrappedElement.GetType() == typeof(DummyWebElement)))
                        return;
                    ((DummyWebElement)((NgWebElement)this.element).WrappedElement).TagName = value;
                }
            }
        }

        public string Text
        {
            get
            {
                this.InitializeWebElement();
                string text;
                try
                {
                    text = this.element.Text;
                }
                catch (WebDriverException ex)
                {
                    this.Report.Write("Attempted to get the inner text of this element by: '" + this.By.ToString() + "', but failed.");
                    throw new Exception("Attempted to get the inner text of this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
                }
                this.Report.Write("Got the inner text: '" + text + "' of this element by: '" + this.By.ToString() + "'.");
                return text;
            }
            set
            {
                this.InitializeWebElement();
                if (this.element.GetType() == typeof(DummyWebElement))
                {
                    ((DummyWebElement)this.element).Text = value;
                }
                else
                {
                    if (!(this.element.GetType() == typeof(NgWebElement)) || !(((NgWebElement)this.element).WrappedElement.GetType() == typeof(DummyWebElement)))
                        return;
                    ((DummyWebElement)((NgWebElement)this.element).WrappedElement).Text = value;
                }
            }
        }

        public ICoordinates Coordinates
        {
            get
            {
                this.InitializeWebElement();
                ICoordinates coordinates;
                try
                {
                    coordinates = ((ILocatable)((IWrapsElement)this.element).WrappedElement).Coordinates;
                }
                catch (WebDriverException ex)
                {
                    this.Report.Write("Attempted to get the coordinates of this element by: '" + this.By.ToString() + "', but failed.");
                    throw new Exception("Attempted to get the coordinates of this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
                }
                this.Report.Write("Got the coordinates: '" + (object)coordinates + "' of this element by: '" + this.By.ToString() + "'.");
                this.Report.Write("Got the coordinates AuxiliaryLocator: '" + coordinates.AuxiliaryLocator + "' of this element by: '" + this.By.ToString() + "'.");
                this.Report.Write("Got the coordinates LocationInDom: '" + (object)coordinates.LocationInDom + "' of this element by: '" + this.By.ToString() + "'.");
                this.Report.Write("Got the coordinates LocationInViewport: '" + (object)coordinates.LocationInViewport + "' of this element by: '" + this.By.ToString() + "'.");
                return coordinates;
            }
            set
            {
                this.InitializeWebElement();
                if (this.element.GetType() == typeof(DummyWebElement))
                {
                    ((DummyWebElement)this.element).Coordinates = value;
                }
                else
                {
                    if (!(this.element.GetType() == typeof(NgWebElement)) || !(((NgWebElement)this.element).WrappedElement.GetType() == typeof(DummyWebElement)))
                        return;
                    ((DummyWebElement)((NgWebElement)this.element).WrappedElement).Coordinates = value;
                }
            }
        }

        public Point LocationOnScreenOnceScrolledIntoView
        {
            get
            {
                this.InitializeWebElement();
                Point scrolledIntoView;
                try
                {
                    scrolledIntoView = ((ILocatable)((IWrapsElement)this.element).WrappedElement).LocationOnScreenOnceScrolledIntoView;
                }
                catch (WebDriverException ex)
                {
                    this.Report.Write("Attempted to get the Point of this element by: '" + this.By.ToString() + "', but failed.");
                    throw new Exception("Attempted to get the Point of this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
                }
                this.Report.Write("Got the Point: '" + (object)scrolledIntoView + "' of this element by: '" + this.By.ToString() + "'.");
                return scrolledIntoView;
            }
            set
            {
                this.InitializeWebElement();
                if (this.element.GetType() == typeof(DummyWebElement))
                {
                    ((DummyWebElement)this.element).LocationOnScreenOnceScrolledIntoView = value;
                }
                else
                {
                    if (!(this.element.GetType() == typeof(NgWebElement)) || !(((NgWebElement)this.element).WrappedElement.GetType() == typeof(DummyWebElement)))
                        return;
                    ((DummyWebElement)((NgWebElement)this.element).WrappedElement).LocationOnScreenOnceScrolledIntoView = value;
                }
            }
        }

        public string FakeAttributeId
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeId = ((DummyWebElement)this.element).FakeAttributeId;
                }
                return this._fakeAttributeId;
            }
            set
            {
                this._fakeAttributeId = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeId = this._fakeAttributeId;
            }
        }

        public string FakeAttributeName
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeName = ((DummyWebElement)this.element).FakeAttributeName;
                }
                return this._fakeAttributeName;
            }
            set
            {
                this._fakeAttributeName = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeName = this._fakeAttributeName;
            }
        }

        public string FakeAttributeValue
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeValue = ((DummyWebElement)this.element).FakeAttributeValue;
                }
                return this._fakeAttributeValue;
            }
            set
            {
                this._fakeAttributeValue = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeValue = this._fakeAttributeValue;
            }
        }

        public string FakeAttributeClass
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeClass = ((DummyWebElement)this.element).FakeAttributeClass;
                }
                return this._fakeAttributeClass;
            }
            set
            {
                this._fakeAttributeClass = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeClass = this._fakeAttributeClass;
            }
        }

        public string FakeAttributeHref
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeHref = ((DummyWebElement)this.element).FakeAttributeHref;
                }
                return this._fakeAttributeHref;
            }
            set
            {
                this._fakeAttributeHref = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeHref = this._fakeAttributeHref;
            }
        }

        public string FakeAttributeStyle
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeStyle = ((DummyWebElement)this.element).FakeAttributeStyle;
                }
                return this._fakeAttributeStyle;
            }
            set
            {
                this._fakeAttributeStyle = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeStyle = this._fakeAttributeStyle;
            }
        }

        public string FakeAttributeOnClick
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeOnClick = ((DummyWebElement)this.element).FakeAttributeOnClick;
                }
                return this._fakeAttributeOnClick;
            }
            set
            {
                this._fakeAttributeOnClick = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeOnClick = this._fakeAttributeOnClick;
            }
        }

        public string FakeAttributeDocid
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeDocid = ((DummyWebElement)this.element).FakeAttributeDocid;
                }
                return this._fakeAttributeDocid;
            }
            set
            {
                this._fakeAttributeDocid = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeDocid = this._fakeAttributeDocid;
            }
        }

        public string FakeAttributeNk
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeNk = ((DummyWebElement)this.element).FakeAttributeNk;
                }
                return this._fakeAttributeNk;
            }
            set
            {
                this._fakeAttributeNk = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeNk = this._fakeAttributeNk;
            }
        }

        public string FakeAttributeKey
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeKey = ((DummyWebElement)this.element).FakeAttributeKey;
                }
                return this._fakeAttributeKey;
            }
            set
            {
                this._fakeAttributeKey = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeKey = this._fakeAttributeKey;
            }
        }

        public string FakeAttributeItemid
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeItemid = ((DummyWebElement)this.element).FakeAttributeItemid;
                }
                return this._fakeAttributeItemid;
            }
            set
            {
                this._fakeAttributeItemid = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeItemid = this._fakeAttributeItemid;
            }
        }

        public string FakeAttributePassageid
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributePassageid = ((DummyWebElement)this.element).FakeAttributePassageid;
                }
                return this._fakeAttributePassageid;
            }
            set
            {
                this._fakeAttributePassageid = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributePassageid = this._fakeAttributePassageid;
            }
        }

        public string FakeAttributeRubricid
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeRubricid = ((DummyWebElement)this.element).FakeAttributeRubricid;
                }
                return this._fakeAttributeRubricid;
            }
            set
            {
                this._fakeAttributeRubricid = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeRubricid = this._fakeAttributeRubricid;
            }
        }

        public string FakeAttributeDataKey
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeDataKey = ((DummyWebElement)this.element).FakeAttributeDataKey;
                }
                return this._fakeAttributeDataKey;
            }
            set
            {
                this._fakeAttributeDataKey = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeDataKey = this._fakeAttributeDataKey;
            }
        }

        public string FakeAttributeDataNgBind
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeDataNgBind = ((DummyWebElement)this.element).FakeAttributeDataNgBind;
                }
                return this._fakeAttributeDataNgBind;
            }
            set
            {
                this._fakeAttributeDataNgBind = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeDataNgBind = this._fakeAttributeDataNgBind;
            }
        }

        public string FakeAttributeDataNgClass
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeDataNgClass = ((DummyWebElement)this.element).FakeAttributeDataNgClass;
                }
                return this._fakeAttributeDataNgClass;
            }
            set
            {
                this._fakeAttributeDataNgClass = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeDataNgClass = this._fakeAttributeDataNgClass;
            }
        }

        public string FakeAttributeDataNgClick
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeDataNgClick = ((DummyWebElement)this.element).FakeAttributeDataNgClick;
                }
                return this._fakeAttributeDataNgClick;
            }
            set
            {
                this._fakeAttributeDataNgClick = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeDataNgClick = this._fakeAttributeDataNgClick;
            }
        }

        public string FakeAttributeDataNgIf
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeDataNgIf = ((DummyWebElement)this.element).FakeAttributeDataNgIf;
                }
                return this._fakeAttributeDataNgIf;
            }
            set
            {
                this._fakeAttributeDataNgIf = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeDataNgIf = this._fakeAttributeDataNgIf;
            }
        }

        public string FakeAttributeDataNgLabel
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeDataNgLabel = ((DummyWebElement)this.element).FakeAttributeDataNgLabel;
                }
                return this._fakeAttributeDataNgLabel;
            }
            set
            {
                this._fakeAttributeDataNgLabel = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeDataNgLabel = this._fakeAttributeDataNgLabel;
            }
        }

        public string FakeAttributeDataNgModel
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeDataNgModel = ((DummyWebElement)this.element).FakeAttributeDataNgModel;
                }
                return this._fakeAttributeDataNgModel;
            }
            set
            {
                this._fakeAttributeDataNgModel = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeDataNgModel = this._fakeAttributeDataNgModel;
            }
        }

        public string FakeAttributeDataNgOptions
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeDataNgOptions = ((DummyWebElement)this.element).FakeAttributeDataNgOptions;
                }
                return this._fakeAttributeDataNgOptions;
            }
            set
            {
                this._fakeAttributeDataNgOptions = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeDataNgOptions = this._fakeAttributeDataNgOptions;
            }
        }

        public string FakeAttributeDataNgRepeat
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeDataNgRepeat = ((DummyWebElement)this.element).FakeAttributeDataNgRepeat;
                }
                return this._fakeAttributeDataNgRepeat;
            }
            set
            {
                this._fakeAttributeDataNgRepeat = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeDataNgRepeat = this._fakeAttributeDataNgRepeat;
            }
        }

        public string FakeAttributeNgIf
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeNgIf = ((DummyWebElement)this.element).FakeAttributeNgIf;
                }
                return this._fakeAttributeNgIf;
            }
            set
            {
                this._fakeAttributeNgIf = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeNgIf = this._fakeAttributeNgIf;
            }
        }

        public string FakeAttributeAriaValueNow
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeAriaValueNow = ((DummyWebElement)this.element).FakeAttributeAriaValueNow;
                }
                return this._fakeAttributeAriaValueNow;
            }
            set
            {
                this._fakeAttributeAriaValueNow = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeAriaValueNow = this._fakeAttributeAriaValueNow;
            }
        }

        public string FakeAttributeAriaLabel
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeAriaLabel = ((DummyWebElement)this.element).FakeAttributeAriaLabel;
                }
                return this._fakeAttributeAriaLabel;
            }
            set
            {
                this._fakeAttributeAriaLabel = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeAriaLabel = this._fakeAttributeAriaLabel;
            }
        }

        public string FakeAttributeRid
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeRid = ((DummyWebElement)this.element).FakeAttributeRid;
                }
                return this._fakeAttributeRid;
            }
            set
            {
                this._fakeAttributeRid = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeRid = this._fakeAttributeRid;
            }
        }

        public string FakeAttributeDetail
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeDetail = ((DummyWebElement)this.element).FakeAttributeDetail;
                }
                return this._fakeAttributeDetail;
            }
            set
            {
                this._fakeAttributeDetail = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeDetail = this._fakeAttributeDetail;
            }
        }

        public string FakeAttributeTitle
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeTitle = ((DummyWebElement)this.element).FakeAttributeTitle;
                }
                return this._fakeAttributeTitle;
            }
            set
            {
                this._fakeAttributeTitle = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeTitle = this._fakeAttributeTitle;
            }
        }

        public string FakeAttributeContent
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeAttributeContent = ((DummyWebElement)this.element).FakeAttributeContent;
                }
                return this._fakeAttributeContent;
            }
            set
            {
                this._fakeAttributeContent = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeAttributeContent = this._fakeAttributeContent;
            }
        }

        public string FakeCssValueStyle
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeCssValueStyle = ((DummyWebElement)this.element).FakeCssValueStyle;
                }
                return this._fakeCssValueStyle;
            }
            set
            {
                this._fakeCssValueStyle = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeCssValueStyle = this._fakeCssValueStyle;
            }
        }

        public string FakeCssValueClass
        {
            get
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver))
                {
                    this.InitializeWebElement();
                    this._fakeCssValueClass = ((DummyWebElement)this.element).FakeCssValueClass;
                }
                return this._fakeCssValueClass;
            }
            set
            {
                this._fakeCssValueClass = value;
                if (!(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)))
                    return;
                this.InitializeWebElement();
                ((DummyWebElement)this.element).FakeCssValueClass = this._fakeCssValueClass;
            }
        }

        public List<IWebElement> FakeFindElementsList { get; set; }

        public List<NgWebElement> FakeFindNgElementsList { get; set; }

        public WebElementWrapper(By by)
        {
            this.element = (IWebElement)null;
            this.By = by;
            this.testApplication = BaseTestThread.GetTestThread();
            this.testConfiguration = this.testApplication.TestConfiguration;
            this.checkIfNgWebElement();
        }

        private bool checkIfNgWebElement()
        {
            if (this.By != (By)null)
                this.isNgWebElement = this.By.GetType().ToString().Equals("Protractor.JavaScriptBy");
            return this.isNgWebElement;
        }

        public void InitializeWebElement()
        {
            if (!(this.By != (By)null))
                throw new Exception("The By was not initialized.");
            if (this.WrappedElement != null)
                return;
            if (this.isNgWebElement)
                this.WrappedElement = (IWebElement)this.Driver.NgWebDriver.FindElement(this.By);
            else
                this.WrappedElement = this.Driver.FindElement(this.By);
        }

        private ReadOnlyCollection<IWebElement> WaitForElements(By by, double timeout)
        {
            this.Report.Write("Wait up to '" + (object)timeout + "' seconds to find the elements by: '" + by.ToString() + "'.");
            ReadOnlyCollection<IWebElement> readOnlyCollection;
            try
            {
                readOnlyCollection = new WebDriverWait((IWebDriver)this.Driver, TimeSpan.FromSeconds(timeout)).Until<ReadOnlyCollection<IWebElement>>((Func<IWebDriver, ReadOnlyCollection<IWebElement>>)(driver => driver.FindElements(by)));
            }
            catch (WebDriverTimeoutException ex)
            {
                this.Report.Write("Attempted to wait up to '" + (object)timeout + "' seconds to find the elements by: '" + by.ToString() + "', but failed.");
                throw new Exception("Attempted to wait up to '" + (object)timeout + "' seconds to find the elements by: '" + by.ToString() + "', but failed.", (Exception)ex);
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to wait up to '" + (object)timeout + "' seconds to find the elements by: '" + by.ToString() + "', but failed.");
                throw new Exception("Attempted to wait up to '" + (object)timeout + "' seconds to find the elements by: '" + by.ToString() + "', but failed.", (Exception)ex);
            }
            this.Report.Write("Found the elements by: '" + by.ToString() + "'.");
            return readOnlyCollection;
        }

        public ReadOnlyCollection<IWebElement> WaitForElements(double timeout)
        {
            return this.WaitForElements(this.By, timeout);
        }

        public ReadOnlyCollection<IWebElement> WaitForElements()
        {
            return this.WaitForElements(this.By, 90.0);
        }

        private IWebElement WaitForElement(By by, double timeout)
        {
            this.Report.Write("Wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "'.");
            IWebElement webElement;
            try
            {
                webElement = !(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)) ? new WebDriverWait((IWebDriver)this.Driver, TimeSpan.FromSeconds(timeout)).Until<IWebElement>(ExpectedConditions.ElementExists(by)) : this.Driver.FindElement(by);
            }
            catch (WebDriverTimeoutException ex)
            {
                this.Report.Write("Attempted to wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.");
                throw new Exception("Attempted to wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.", (Exception)ex);
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.");
                throw new Exception("Attempted to wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.", (Exception)ex);
            }
            this.Report.Write("Found the element by: '" + by.ToString() + "'.");
            return webElement;
        }

        private IWebElement WaitForElement(By by)
        {
            return this.WaitForElement(by, 90.0);
        }

        private IWebElement WaitForVisibleElement(By by, double timeout)
        {
            this.Report.Write("Wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "'.");
            IWebElement webElement;
            try
            {
                webElement = !(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)) ? new WebDriverWait((IWebDriver)this.Driver, TimeSpan.FromSeconds(timeout)).Until<IWebElement>(ExpectedConditions.ElementIsVisible(by)) : this.Driver.FindElement(by);
            }
            catch (WebDriverTimeoutException ex)
            {
                this.Report.Write("Attempted to wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.");
                throw new Exception("Attempted to wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.", (Exception)ex);
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.");
                throw new Exception("Attempted to wait up to '" + (object)timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.", (Exception)ex);
            }
            this.Report.Write("Found the element by: '" + by.ToString() + "'.");
            return webElement;
        }

        private IWebElement WaitForVisibleElement(By by)
        {
            return this.WaitForVisibleElement(by, 90.0);
        }

        public WebElementWrapper Wait(double timeout)
        {
            this.element = this.WaitForVisibleElement(this.By, timeout);
            return this;
        }

        public WebElementWrapper Wait()
        {
            this.element = this.WaitForVisibleElement(this.By);
            return this;
        }

        public WebElementWrapper WaitUntilVisible(double timeout)
        {
            this.element = this.WaitForVisibleElement(this.By, timeout);
            return this;
        }

        public WebElementWrapper WaitUntilVisible()
        {
            this.element = this.WaitForVisibleElement(this.By);
            return this;
        }

        public WebElementWrapper WaitUntilExists(double timeout)
        {
            this.element = this.WaitForElement(this.By, timeout);
            return this;
        }

        public WebElementWrapper WaitUntilExists()
        {
            this.element = this.WaitForElement(this.By);
            return this;
        }

        public void Clear()
        {
            this.InitializeWebElement();
            try
            {
                this.element.Clear();
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to clear the content of this element by: '" + this.By.ToString() + "', but failed.");
                throw new Exception("Attempted to clear the content of this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
            }
            this.Report.Write("Cleared the content of this element by: '" + this.By.ToString() + "'.");
        }

        public void Click()
        {
            this.InitializeWebElement();
            try
            {
                this.element.Click();
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to click this element by: '" + this.By.ToString() + "', but failed.");
                throw new Exception("Attempted to click this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
            }
            this.Report.Write("Clicked this element by: '" + this.By.ToString() + "'.");
        }

        public string GetAttribute(string attributeName)
        {
            this.InitializeWebElement();
            string attribute;
            try
            {
                attribute = this.element.GetAttribute(attributeName);
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to get the attribute: '" + attributeName + "' of this element by: '" + this.By.ToString() + "', but failed.");
                throw new Exception("Attempted to get the attribute: '" + attributeName + "' of this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
            }
            if (attribute == null)
                this.Report.Write("Got the attribute: '" + attributeName + "' of this element by: '" + this.By.ToString() + "'.");
            else
                this.Report.Write("Got the attribute: '" + attributeName + "' and value: '" + attribute + "' of this element by: '" + this.By.ToString() + "'.");
            return attribute;
        }

        public string GetCssValue(string propertyName)
        {
            this.InitializeWebElement();
            string cssValue;
            try
            {
                cssValue = this.element.GetCssValue(propertyName);
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to get the CSS value: '" + propertyName + "' of this element by: '" + this.By.ToString() + "', but failed.");
                throw new Exception("Attempted to get the CSS value: '" + propertyName + "' of this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
            }
            if (cssValue == null)
                this.Report.Write("Got the CSS property: '" + propertyName + "' of this element by: '" + this.By.ToString() + "'.");
            else
                this.Report.Write("Got the CSS property: '" + propertyName + "' and value: '" + cssValue + "' of this element by: '" + this.By.ToString() + "'.");
            return cssValue;
        }

        public void SendKeys(string text)
        {
            this.InitializeWebElement();
            try
            {
                this.element.SendKeys(text);
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to type the text: '" + text + "' into this element by: '" + this.By.ToString() + "', but failed.");
                throw new Exception("Attempted to type the text: '" + text + "' into this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
            }
            this.Report.Write("Typed the text: '" + text + "' into this element by: '" + this.By.ToString() + "'.");
        }

        public void Submit()
        {
            this.InitializeWebElement();
            try
            {
                this.element.Submit();
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to submit this element by: '" + this.By.ToString() + "', but failed.");
                throw new Exception("Attempted to submit this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
            }
            this.Report.Write("Submitted this element by: '" + this.By.ToString() + "'.");
        }

        public void MoveToElement()
        {
            this.InitializeWebElement();
            try
            {
                new Actions((IWebDriver)this.Driver).MoveToElement(this.element).Perform();
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to move to this element by: '" + this.By.ToString() + "', but failed.");
                throw new Exception("Attempted to move to this element by: '" + this.By.ToString() + "', but failed.", (Exception)ex);
            }
            this.Report.Write("Move to this element by: '" + this.By.ToString() + "'.");
        }

        public void MouseOverAndClick(WebElementWrapper elementToClick)
        {
            this.InitializeWebElement();
            try
            {
                new Actions((IWebDriver)this.Driver).MoveToElement(this.element).MoveToElement(elementToClick.element).Click().Build().Perform();
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to mouse over this element by: '" + this.By.ToString() + "' and click this element by: '" + elementToClick.By.ToString() + "', but failed.");
                throw new Exception("Attempted to mouse over this element by: '" + this.By.ToString() + "' and click this element by: '" + elementToClick.By.ToString() + "', but failed.", (Exception)ex);
            }
            this.Report.Write("Mouse over and click this element by: '" + this.By.ToString() + "'.");
        }

        public IWebElement FindElement(By by)
        {
            this.InitializeWebElement();
            IWebElement webElement;
            try
            {
                webElement = !by.GetType().ToString().Equals("Protractor.JavaScriptBy") ? this.WrappedElement.FindElement(by) : (IWebElement)((NgWebElement)this.WrappedElement).FindElement(by);
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to find the element by: '" + by.ToString() + "', but failed.");
                throw new Exception("Attempted to find the element by: '" + by.ToString() + "', but failed.", (Exception)ex);
            }
            this.Report.Write("Found the element by: '" + by.ToString() + "'.");
            return webElement;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            this.InitializeWebElement();
            try
            {
                if (this.Driver.WrappedDriver.GetType() == typeof(DummyDriver) && this.FakeFindElementsList != null)
                    ((DummyWebElement)this.WrappedElement).FakeFindElementsList = this.FakeFindElementsList;
                this.WrappedElements = this.WrappedElement.FindElements(by);
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to find the elements by: '" + by.ToString() + "', but failed.");
                throw new Exception("Attempted to find the elements by: '" + by.ToString() + "', but failed.", (Exception)ex);
            }
            this.Report.Write("Found the elements by: '" + by.ToString() + "'.");
            return this.WrappedElements;
        }

        public ReadOnlyCollection<NgWebElement> FindNgElements(By by)
        {
            this.WrappedElement = (IWebElement)null;
            this.isNgWebElement = true;
            this.InitializeWebElement();
            try
            {
                this.WrappedNgElements = !(this.Driver.WrappedDriver.GetType() == typeof(DummyDriver)) ? ((NgWebElement)this.WrappedElement).FindElements(by) : (this.FakeFindNgElementsList != null ? new ReadOnlyCollection<NgWebElement>((IList<NgWebElement>)this.FakeFindNgElementsList) : new ReadOnlyCollection<NgWebElement>((IList<NgWebElement>)new List<NgWebElement>()));
            }
            catch (WebDriverException ex)
            {
                this.Report.Write("Attempted to find the elements by: '" + by.GetType().ToString() + "', but failed.");
                throw new Exception("Attempted to find the elements by: '" + by.GetType().ToString() + "', but failed.", (Exception)ex);
            }
            this.Report.Write("Found the elements by: '" + by.GetType().ToString() + "'.");
            return this.WrappedNgElements;
        }

        public void Check()
        {
            if (this.Selected)
                return;
            this.Click();
        }

        public void UnCheck()
        {
            this.Selected = true;
            if (!this.Selected)
                return;
            this.Click();
        }
         * */


        public By by { get; set; }

        public ISearchContext SearchContext { get; set; }

        public WebElementWrapper(IWebElement element)
        {
            this.WrappedElement = element;
        }

        public WebElementWrapper(IWebDriver driver, By by)
        {
            this.SearchContext = driver;
            this.by = by;
        }

        public WebElementWrapper(IWebElement parentElement, By by)
        {
            this.SearchContext = parentElement;
            this.by = by;
        }

        public WebElementWrapper(DriverWrapper driver, By by)
        {
            this.SearchContext = driver;
            this.by = by;
        }
        public WebElementWrapper(WebElementWrapper parentElementWrapper, By by)
        {
            this.SearchContext = parentElementWrapper;
            this.by = by;
        }

        public IWebElement WrappedElement
        {
            get {
                return WrappedElement;
            }
            set
            {
                WrappedElement = value;
            }
        }
        
        public ICoordinates Coordinates
        {
            get { return ((ILocatable)WrappedElement).Coordinates; }
        }

        public Point LocationOnScreenOnceScrolledIntoView
        {
            get { return ((ILocatable)WrappedElement).LocationOnScreenOnceScrolledIntoView; }
        }

        public IWebElement FindElement(By by)
        {
            return WrappedElement.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return WrappedElement.FindElements(by);
        }

        public void Clear()
        {
            WrappedElement.Clear();
        }

        public void Click()
        {
            WrappedElement.Click();
        }

        public bool Displayed
        {
            get { return WrappedElement.Displayed; }
        }

        public bool Enabled
        {
            get { return WrappedElement.Enabled; }
        }

        public string GetAttribute(string attributeName)
        {
            return WrappedElement.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return WrappedElement.GetCssValue(propertyName);
        }

        public Point Location
        {
            get { return WrappedElement.Location; }
        }

        public bool Selected
        {
            get { return WrappedElement.Selected; }
        }

        public void SendKeys(string text)
        {
            WrappedElement.SendKeys(text);
        }

        public Size Size
        {
            get { return WrappedElement.Size; }
        }

        public void Submit()
        {
            WrappedElement.Submit();
        }

        public string TagName
        {
            get { return WrappedElement.TagName; }
        }

        public string Text
        {
            get { return WrappedElement.Text; }
        }
    }
}
