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
using Windows.Storage;
using System.Linq;
using System.Reflection;



namespace Bifrost.Execution
{
	/// <summary>
    /// Represents a <see cref="IAssemblyLocator"/>
    /// </summary>
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
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var assemblies = new List<Assembly>();

            IEnumerable<StorageFile>    files = null;

            var operation = folder.GetFilesAsync();
            operation.Completed = async (r, s) => {
                var result = await r;
                files = result;
            };

            while (files == null) ;

            foreach (var file in files)
            {
                if (file.FileType == ".dll" || file.FileType == ".exe")
                {
                    var name = new AssemblyName() { Name = System.IO.Path.GetFileNameWithoutExtension(file.Name) };
                    try
                    {
                        Assembly asm = Assembly.Load(name);
                        assemblies.Add(asm);
                    }
                    catch { }
                }
            }
            _assemblies = assemblies.ToArray();
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