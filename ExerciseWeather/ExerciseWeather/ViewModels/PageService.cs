using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExerciseWeather.ViewModels
{
    public class PageService : IPageService
    {
        private Page MainPage => Application.Current.MainPage;

        public double GetWidth()
        {
            return MainPage.Width;
        }

        public double GetHeight()
        {
            return MainPage.Height;
        }
    }
}
