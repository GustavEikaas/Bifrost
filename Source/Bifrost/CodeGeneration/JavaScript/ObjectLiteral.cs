﻿#region License
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

namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents an object literal
    /// </summary>
    public class ObjectLiteral : Container
    {
#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            var childIndex = 0;
            var numberOfChildren = Children.Count;

            writer.Write("{{");

            if (numberOfChildren > 0)
            {
                writer.Newline();
                writer.Indent();

                foreach (var child in Children)
                {
                    child.Write(writer);
                    childIndex++;

                    if (childIndex > 0 && childIndex < numberOfChildren)
                        writer.Write(",");
                    writer.Newline();
                }

                writer.Unindent();
                writer.WriteWithIndentation("}}");
            }
            else
            {
                writer.Write("}}");
            }
        }
#pragma warning restore 1591
    }
}
