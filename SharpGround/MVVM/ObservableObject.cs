/////////////////////////////////////////////////////////////////////////////
// ver 3.0                                                                  //
// Language:     C++ 11                                                     //
// Application:  Summer Project                                             //
// Summer project 2017                                                      //
//https://www.codeproject.com/Articles/165368/WPF-MVVM-Quick-Start-Tutorial //
// From MVVM tutorial                                                       //
// Author:       Kunal Paliwal, Syracuse University, Summer Project         //
//                (315) 876-8002, kupaliwa@syr.edu                          //
//////////////////////////////////////////////////////////////////////////////
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MVVM
{
    [Serializable]
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpresssion)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpresssion);
            this.RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged(String propertyName)
        {
            VerifyPropertyName(propertyName);
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Warns the developer if this Object does not have a public property with
        /// the specified name. This method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(String propertyName)
        {
            // verify that the property name matches a real,  
            // public, instance property on this Object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                Debug.Fail("Invalid property name: " + propertyName);
            }
        }
    }
}
