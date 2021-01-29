using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes
{
    static class Instance
    {
        // удобнее вместо обчного списка, при добавлении или удалении автоматически будут изменяться все привязанные к этой коллекции объекты
        static public ObservableCollection<Notes> marks1 = new ObservableCollection<Notes>();
        static public ObservableCollection<Notes> marks2 = new ObservableCollection<Notes>();
        static public double l1 = 0, l2 = 0;
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //var text = TextBox.Text;
            //// проверяем есть ли у нас что-то в editor
            //if (text.Length != 0 && text != null)
            //{
            //    // проверяем у какого столбца выше высота, если меньше у 1 то добавляем в 1, также и со 2
            //    if (Instance.l1 <= Instance.l2)
            //    {
            //        Instance.marks1.Add(new Notes() { Text = text });
            //    }
            //    else
            //    {
            //        Instance.marks2.Add(new Notes() { Text = text });
            //    }
            //}
            Navigation.PopAsync();
        }

        public void SetText(string text)
        {
            TextBox.Text = text;
        }

        public string GetText()
        {
            return TextBox.Text;
        }
    }
}