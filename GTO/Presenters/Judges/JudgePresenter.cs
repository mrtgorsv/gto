using System.Collections.Generic;
using GTO.Model.Context;
using GTO.Models;
using GTO.Services.Implementations;

namespace GTO.Presenters.Judges
{
    public class JudgePresenter : EditPresenterBase
    {
        public Judge EditableObject { get; set; }

        protected JudgeService JudgeService;

        public JudgePresenter(int judgeId = 0)
        {
            JudgeService = new JudgeService();
            EditableObject = JudgeService.GetOrCreate(judgeId);
        }


        public List<ComboBoxItem> SexList => new List<ComboBoxItem>
        {
            new ComboBoxItem {Text = "Мужской", Value = 1},
            new ComboBoxItem {Text = "Женский", Value = 0}
        };

        public override void Dispose()
        {
            JudgeService.Dispose();
        }

        public override void Save()
        {
            JudgeService.AddOrUpdate(EditableObject);
        }
    }
}