using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using GTO.Model.Context;
using GTO.Model.Enums;
using GTO.Models;
using GTO.Services.Implementations;

namespace GTO.Presenters
{
    public class GtoEventTestResultPresenter : IDisposable
    {
        private GtoEvent _currentEvent;

        private readonly GtoEventService _gtoEventService;

        private BindingList<GtoEventPlayerRecord> _gtoEventPlayerRecords;
        private List<AgeGroup> _aviableAgeGroups;

        public GtoEventTestResultPresenter()
        {
            _gtoEventService = new GtoEventService();

            _currentEvent = _gtoEventService.GetCurrentEventDetail();
            _aviableAgeGroups = _gtoEventService.GetAgeGroups();

            InitEventPlayerRecords();
        }

        private void InitEventPlayerRecords()
        {
            _gtoEventPlayerRecords = new BindingList<GtoEventPlayerRecord>(_gtoEventService.GetCurrentEventRecords());
        }

        public BindingList<GtoEventPlayerRecord> EventPlayerRecordDataSource
        {
            get { return _gtoEventPlayerRecords; }
        }

        public List<ComboBoxItem> GtoEventTestDataSource
        {
            get
            {
                return _currentEvent.GtoEventTests.Select(gete => new ComboBoxItem()
                    {
                        Value = gete.Id,
                        Text = gete.TestName
                    })
                    .ToList();
            }
        }


        public void Save()
        {
            _gtoEventService.SaveEventRecords(EventPlayerRecordDataSource.ToList());
        }

        public void Dispose()
        {
            _gtoEventService.Dispose();
        }

        public List<ComboBoxItem> GetAviableEventTests(GtoEventPlayerRecord record)
        {
            var age = record.GtoEventPlayer.Player.Age;
            var sex = record.GtoEventPlayer.Player.Sex;

            var ageGroup = _aviableAgeGroups.FirstOrDefault(ag => ag.Max >= age && ag.Min <= age);
            if (ageGroup == null)
            {
                return new List<ComboBoxItem>();
            }
            var aviableTestIds =
                ageGroup.TestGroups.Where(tg => tg.Sex == sex || tg.Sex == 2).Select(tg => tg.TestId).ToList();

            return _currentEvent.GtoEventTests.Where(gtet => aviableTestIds.Contains(gtet.TestId)).Select(
                gtet => new ComboBoxItem()
                {
                    Value = gtet.Id,
                    Text = gtet.TestName
                }).ToList();
        }

        public void UpdateRecordTest(GtoEventPlayerRecord record)
        {
            GtoEventTest newTest = _currentEvent.GtoEventTests.FirstOrDefault(gete => gete.Id == record.GtoEventTestId);
            if (newTest == record.GtoEventTest)
            {
                return;
            }
            record.GtoEventTest = newTest;
            record.TestValue = string.Empty;
        }

        public void UpdateRecordResult(GtoEventPlayerRecord record)
        {
            if (!string.IsNullOrEmpty(record.TestValue))
            {
                var age = record.GtoEventPlayer.Player.Age;
                var sex = record.GtoEventPlayer.Player.Sex;

                var ageGroup = _aviableAgeGroups.FirstOrDefault(ag => ag.Max >= age && ag.Min <= age);

                if (ageGroup == null)
                {
                    throw new NullReferenceException("Для текущего пользователя нет группы");
                }

                var testGroup =
                    ageGroup.TestGroups.FirstOrDefault(
                        tg => tg.TestId == record.GtoEventTest.TestId && (tg.Sex == 2 || tg.Sex == sex));
                if (testGroup == null)
                {
                    throw new NullReferenceException("Для текущего испытания нет доступных значений для рассчета");
                }

                switch (testGroup.TestType)
                {
                    case TestType.DoubleValue:
                        record.ResultRank = CheckDoubleResult(record.TestValue, testGroup);
                        break;
                    case TestType.IntValue:
                        record.ResultRank = CheckIntResult(record.TestValue, testGroup);
                        break;
                    case TestType.BoolValue:
                        record.ResultRank = CheckBoolResult(record.TestValue, testGroup);
                        break;
                    case TestType.TimeValue:
                        record.ResultRank = CheckTimeResult(record.TestValue, testGroup);
                        break;
                    default:
                        break;
                }
            }
        }

        private ResultRank CheckTimeResult(string record, TestGroup testGroup)
        {
            DateTime? resultTime;
            try
            {
                resultTime = DateTime.ParseExact(record, new[] {"hh:mm:ss", "hh.mm.ss", "mm.ss", "mm:ss", "m.ss", "m:ss" }, CultureInfo.InvariantCulture,
                    DateTimeStyles.None);
            }
            catch (Exception)
            {
                throw new ArgumentException("Некорректное значение. Доступные форматы : 00:00:00 , 00.00.00 , 00.00 , 00:00");
            }
            int minutes = resultTime.Value.Minute;
            int seconds = resultTime.Value.Second;
            if (resultTime.Value.Hour > 0)
            {
                return ResultRank.NoRank;
            }
            double result = minutes + seconds / 100;

            if (!testGroup.Gold.HasValue || result <= testGroup.Gold)
            {
                return ResultRank.Gold;
            }
            if (!testGroup.Silver.HasValue || result <= testGroup.Silver)
            {
                return ResultRank.Serebro;
            }
            if (!testGroup.Bronze.HasValue || result <= testGroup.Bronze)
            {
                return ResultRank.Bronze;
            }

            return ResultRank.NoRank;
        }

        private ResultRank CheckBoolResult(string record, TestGroup testGroup)
        {
            bool result;
            if (bool.TryParse(record, out result))
            {
                return result ? ResultRank.Gold : ResultRank.NoRank;
            }
            if (record.ToLower().Equals("Да".ToLower()))
            {
                return ResultRank.Gold;
            }
            if (record.ToLower().Equals("Нет".ToLower()))
            {
                return ResultRank.NoRank;
            }
            throw new ArgumentException("Некорректное значение. Доступные значения : 0 или 'Нет' , 1 или 'Да'");
        }

        private ResultRank CheckIntResult(string record, TestGroup testGroup)
        {
            int result;
            if (int.TryParse(record, out result))
            {
                if (!testGroup.Gold.HasValue || result >= testGroup.Gold)
                {
                    return ResultRank.Gold;
                }
                if (!testGroup.Silver.HasValue || result >= testGroup.Silver)
                {
                    return ResultRank.Serebro;
                }
                if (!testGroup.Bronze.HasValue || result >= testGroup.Bronze)
                {
                    return ResultRank.Bronze;
                }
                return ResultRank.NoRank;
            }
            throw new ArgumentException("Некорректное значение. Доступные значения : целые числа");
        }

        private ResultRank CheckDoubleResult(string record, TestGroup testGroup)
        {
            double result;
            if (double.TryParse(record, NumberStyles.Any , CultureInfo.InvariantCulture , out  result))
            {
                if (!testGroup.Gold.HasValue || result <= testGroup.Gold)
                {
                    return ResultRank.Gold;
                }
                if (!testGroup.Silver.HasValue || result <= testGroup.Silver)
                {
                    return ResultRank.Serebro;
                }
                if (!testGroup.Bronze.HasValue || result <= testGroup.Bronze)
                {
                    return ResultRank.Bronze;
                }
                return ResultRank.NoRank;
            }
            throw new ArgumentException("Некорректное значение. Доступные значения : числа типа '00.00'");
        }
    }

    public class GtoEventTestRecodDto
    {
        public int GtoPlayerId { get; set; }
        public int? GtoTestId { get; set; }
    }
}