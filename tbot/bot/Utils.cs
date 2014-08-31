using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace tbot.bot{
    public static class Utils{
        public static void AppendText(this RichTextBox box, string text, Color color){
            var rangeOfText1 = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd){Text = text};
            rangeOfText1.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
        }
    }
}