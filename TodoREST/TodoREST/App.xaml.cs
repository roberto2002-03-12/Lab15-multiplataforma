using System;
using TodoREST.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoREST
{
    public partial class App : Application
    {
        public static TodoItemMan TodoManager { get; private set; }
        public App()
        {
            InitializeComponent();
            TodoManager = new TodoItemMan(new RestService());
            MainPage = new NavigationPage(new Views.TodoListPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
