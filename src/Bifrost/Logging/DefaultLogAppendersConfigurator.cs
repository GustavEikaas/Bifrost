/*---------------------------------------------------------------------------------------------
*  Copyright (c) 2008-2017 Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
using Microsoft.Extensions.Logging;

namespace Bifrost.Logging
{
    /// <summary>
    /// Represents the default <see cref="ICanConfigureLogAppenders">configurator</see> for <see cref="ILogAppenders"/>
    /// </summary>
    public class DefaultLogAppendersConfigurator : ICanConfigureLogAppenders
    {
        ILoggerFactory _loggerFactory;

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultLogAppendersConfigurator"/>
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> to use</param>
        public DefaultLogAppendersConfigurator(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }


        /// <inheritdoc/>
        public void Configure(ILogAppenders appenders)
        {
            var defaultLogAppender = new DefaultLogAppender(_loggerFactory);
            appenders.Add(defaultLogAppender);
        }
    }
}
