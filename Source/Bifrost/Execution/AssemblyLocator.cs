#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Bifrost.Execution
{
	/// <summary>
    /// Represents a <see cref="IAssemblyLocator"/>
    /// </summary>
    [Singleton]
    public class AssemblyLocator : IAssemblyLocator
    {
        Assembly[] _assemblies;

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblyLocator"/>
        /// </summary>
        public AssemblyLocator()
        {
            Initialize();
        }

        private void Initialize()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new Uri(codeBase);
            var path = Path.GetDirectoryName(uri.LocalPath);

            IEnumerable<FileInfo> files = new DirectoryInfo(path).GetFiles("*.dll");
            files.Concat(new DirectoryInfo(path).GetFiles("*.exe"));
            files = files.Where(AssemblyDetails.IsAssembly);

            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            foreach (var file in files)
            {
                var assemblyName = AssemblyName.GetAssemblyName(file.FullName);
                if (!currentAssemblies.Any(assembly => Matches(assemblyName, assembly.GetName())))
                    currentAssemblies.Add(Assembly.Load(assemblyName));
            }
            _assemblies = currentAssemblies.Distinct(new AssemblyComparer()).ToArray();
        }

#pragma warning disable 1591 // Xml Comments
        public Assembly[] GetAll()
        {
            return _assemblies;
        }

        public Assembly GetWithFullName(string fullName)
        {
            var query = from a in _assemblies
                        where a.FullName == fullName
                        select a;

            var assembly = query.SingleOrDefault();
            return assembly;
        }

        public Assembly GetWithName(string name)
        {
            var query = from a in _assemblies
                        where a.FullName.Contains(name)
                        select a;

            var assembly = query.SingleOrDefault();
            return assembly;
        }

#pragma warning restore 1591 // Xml Comments

        bool Matches(AssemblyName a, AssemblyName b)
        {
            return a.ToString() == b.ToString();
        }
    }
}