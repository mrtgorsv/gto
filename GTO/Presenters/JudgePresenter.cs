using System;
using System.Collections.Generic;
using GTO.Model.Context;
using GTO.Models;
using GTO.Services.Implementations;

namespace GTO.Presenters
{
    public class JudgePresenter : IDisposable
    {
        public Judge EditableObject { get; set; }
        protected JudgeService JudgeService;

        public JudgePresenter()
        {
            JudgeService = new JudgeService();
            EditableObject = JudgeService.Create();
        }

        public void Save()
        {
            JudgeService.Add(EditableObject);
        }

        public List<ComboBoxItem> SexList
        {
            get
            {
                return new List<ComboBoxItem>
                {
                    new ComboBoxItem {Text = "Мужской" , Value = 1},
                    new ComboBoxItem {Text = "Женский" , Value = 0}
                };
            }
        }

        public void Dispose()
        {
            JudgeService.Dispose();
        }
    }
}