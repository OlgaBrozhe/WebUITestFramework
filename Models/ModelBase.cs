// <author>Olga Brozhe.</author>
//<summary>Base class for models.</summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebUITesting.Pages;

namespace WebUITesting.Models
{
    [TestClass]
    public abstract class ModelBase : PageBase
    {
        /// <summary>
        /// Gets/sets the PrimaryHeader.
        /// </summary>
        internal PrimaryHeader PrimaryHeader { get; set; }
    }
}