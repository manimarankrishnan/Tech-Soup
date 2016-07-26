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
        public static const String WaitForAngular = @"var rootSelector = arguments[0];
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
        /// Pass webelement as the first argument
        /// </summary>
        public static const String ScrollToElement = "arguments[0].scrollIntoView(true);";



    }
}
