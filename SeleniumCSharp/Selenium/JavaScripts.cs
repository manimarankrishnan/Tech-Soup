using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharp.Selenium
{
    public class JavaScripts
    {

        /// <summary>
        /// Pass locator for ng-app root element as first argument
        /// </summary>
        public const  String WaitForAngular = @"var rootSelector = arguments[0];
                                                    var el = document.querySelector(rootSelector);
                                                    var callback = arguments[1];
                                                    if (window.getAngularTestability) {
                                                        window.getAngularTestability(el).whenStable(callback);
                                                        return;
                                                    }
                                                    if (!window.angular) {
                                                    console.log('no angular present. returning');
                                                       return;
                                                    }
                                                    if (angular.getTestability) {
                                                        angular.getTestability(el).whenStable(callback);
                                                    } else {
                                                        if (!angular.element(el).injector()) {
                                                            throw new Error('root element (' + rootSelector + ') has no injector.' +
                                                                ' this may mean it is not inside ng-app.');
                                                        }
                                                    angular.element(el).injector().get('$browser').
                                                        notifyWhenNoOutstandingRequests(callback);
                                                    }";

        /// <summary>
        /// arguments[0] - IWebelement element - the element to scroll into view
        /// </summary>
        public const  String ScrollIntoView = "arguments[0].scrollIntoView(true);";

        /// <summary>
        /// arguments[0] - int offset - Y axis position, where to scroll
        /// </summary>
        public const String ScrollVeriticallyTo = "scrollTo(0,arguments[0]);";


        /// <summary>
        /// arguments[0] - int offset - X axis position, where to scroll
        /// </summary>
        public const String ScrollHorizontallyTo = "scrollTo(arguments[0],0);";

        /// <summary>
        /// arguments[0] - String eventName - name of the event 
        /// arguments[1] - String functionBody - the function body of the event listener
        /// </summary>
        public const String AddEventListenerStringFormat = "window.addEventListener('{0}',function(event){{1}});";

        /// <summary>
        /// Returns the javascript to add a function to a eventListener
        /// </summary>
        /// <param name="eventName">name of the event</param>
        /// <param name="functionBody">the function body of the event listener</param>
        /// <returns></returns>
        public static String GetJSToAddEventListener(String eventName, String functionBody)
        {
            return String.Format(AddEventListenerStringFormat, eventName, functionBody);
        }

        /// <summary>
        /// Returns the parent element 
        /// arguments[0] - IWebElement element - the webelement
        /// </summary>
        public const String ParentElementNode = "return arguments[0].parentNode;";

    }
}
