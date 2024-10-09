/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Bifrost.Web
{
    public interface IWebContext
    {
        IWebRequest Request { get; }

        void RewritePath(string path);
        bool HasRouteForCurrentRequest { get; }
    }
}
