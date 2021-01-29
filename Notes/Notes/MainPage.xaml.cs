using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes
{
    public class Notes
    {
        public string Text { get; set; }
        public int Id { get; private set; }
        private static int globalId = 0;

        public Notes()
        {
            Id = ++globalId;
        }
    }
    public partial class MainPage : ContentPage
    {
        bool isOpened = false;
        public MainPage()
        {
            InitializeComponent();
            BindableLayout.SetItemsSource(Col1, Instance.marks1);
            BindableLayout.SetItemsSource(Col2, Instance.marks2);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!isOpened)
            {
                isOpened = true;

                var form = new Page1();
                Navigation.PushAsync(form);
                form.Disappearing += (send, ev) =>
                {
                    isOpened = false;
                    var text = form.GetText();
                    // проверяем есть ли у нас что-то в editor
                    if (text.Length != 0 && text != null)
                    {
                        // проверяем у какого столбца выше высота, если меньше у 1 то добавляем в 1, также и со 2
                        PushNote(text);
                    }
                };
            }
        }
        private void Col1_SizeChanged(object sender, EventArgs e)
        {
            Instance.l1 = Col1.Height;
        }

        private void Col2_SizeChanged(object sender, EventArgs e)
        {
            Instance.l2 = Col2.Height;
        }

        private void PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            //int ID = int.Parse((e as PanUpdatedEventArgs).Parameter.ToString());
            //ObservableCollection<Notes> buf = new ObservableCollection<Notes>();
            //foreach (Notes note in Instance.marks1)
            //{
            //    buf.Add(note);
            //}
            //foreach (Notes note in Instance.marks2)
            //{
            //    buf.Add(note);
            //}
            //Notes my_note = buf.Select(a => { return a; }).Where(a => a.Id == ID).ToList()[0];
            //Instance.marks1.Remove(my_note);
            //Instance.marks2.Remove(my_note);
        }

        private void Tapped(object sender, EventArgs e)
        {
            if (!isOpened)
            {
                isOpened = true;
                int ID = int.Parse((e as TappedEventArgs).Parameter.ToString());
                Notes note = GetNote(ID);

                var form = new Page1();
                form.SetText(note.Text);
                Navigation.PushAsync(form);
                form.Disappearing += (send, ev) =>
                {
                    isOpened = false;
                    if (form.GetText() != note.Text)
                    {
                        if (Instance.marks1.Contains(note))
                        {
                            int index = Instance.marks1.IndexOf(note);
                            Instance.marks1[index].Text = form.GetText();
                            Instance.marks1.Add(new Notes());
                            Instance.marks1.RemoveAt(Instance.marks1.Count - 1);
                        }
                        if (Instance.marks2.Contains(note))
                        {
                            int index = Instance.marks2.IndexOf(note);
                            Instance.marks2[index].Text = form.GetText();
                            Instance.marks2.Add(new Notes());
                            Instance.marks2.RemoveAt(Instance.marks2.Count - 1);
                        }
                    }
                };
            }
        }

        Notes GetNote(int id)
        {
            ObservableCollection<Notes> buf = new ObservableCollection<Notes>();
            foreach (Notes note in Instance.marks1)
            {
                buf.Add(note);
            }
            foreach (Notes note in Instance.marks2)
            {
                buf.Add(note);
            }
            return buf.Select(a => { return a; }).Where(a => a.Id == id).ToList()[0];
        }

        void PushNote(string text)
        {
            if (Instance.l1 <= Instance.l2)
            {
                Instance.marks1.Add(new Notes() { Text = text });
            }
            else
            {
                Instance.marks2.Add(new Notes() { Text = text });
            }
        }
    } 
}
