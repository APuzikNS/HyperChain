using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SemanticLinkHelper;
using System.Windows.Markup;

namespace HyperChain
{
    /// <summary>
    /// Interaction logic for EditRelationsWindow.xaml
    /// </summary>
    public partial class EditRelationsWindow : Window
    {
        WordSemanticBranch m_Link = null;

        public EditRelationsWindow(WordSemanticBranch aLink)
        {
            InitializeComponent();
            m_Link = aLink;
            lvChildren.ItemsSource = aLink.Children;
            this.Title = string.Format("{0} {1}", this.Title, aLink.ParentWord.Word);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SemanticHelper helper = new SemanticHelper();
            helper.UpdateRelations(m_Link);
            m_Link.ParentWord.Reviewed = true;
            helper.UpdateWordStatus(m_Link.ParentWord, m_Link.ParentWord.Status);
            DialogResult = true;
            Close();
        }
    }

    //[MarkupExtensionReturnType(typeof(object[]))]
    //public class EnumValuesExtension : MarkupExtension
    //{
    //    public EnumValuesExtension()
    //    {
    //    }

    //    public EnumValuesExtension(Type enumType)
    //    {
    //        this.EnumType = enumType;
    //    }

    //    [ConstructorArgument("enumType")]
    //    public Type EnumType { get; set; }

    //    public override object ProvideValue(IServiceProvider serviceProvider)
    //    {
    //        if (this.EnumType == null)
    //            throw new ArgumentException("The enum type is not set");
    //        return Enum.GetValues(this.EnumType);
    //    }
    //}
}
