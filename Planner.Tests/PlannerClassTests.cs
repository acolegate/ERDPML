using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Planner.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PlannerClassTests
    {
        private readonly PlannerClass _classUnderTest = PlannerClass.Instance;

        [TestMethod]
        public void PlannerClass_GetMaxForDepth_AllValuesAndEdgeCases()
        {
            new TimeCell(10, 219, 'Z', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(0));
            new TimeCell(10, 219, 'Z', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(1));
            new TimeCell(10, 219, 'Z', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(9));

            new TimeCell(10, 219, 'Z', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(10));
            new TimeCell(12, 147, 'Z', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(12));
            new TimeCell(14, 98, 'Y', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(14));
            new TimeCell(16, 72, 'X', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(16));
            new TimeCell(18, 56, 'W', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(18));
            new TimeCell(20, 45, 'U', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(20));
            new TimeCell(22, 37, 'S', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(22));
            new TimeCell(25, 29, 'Q', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(25));
            new TimeCell(30, 20, 'N', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(30));
            new TimeCell(35, 14, 'K', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(35));
            new TimeCell(40, 9, 'G', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(40));
            new TimeCell(42, 8, 'F', safetyStopRequired: true, noDecompressionLimit: true).AssertHasSamePropertyValues(_classUnderTest.GetMaxForDepth(42));

            Assert.IsNull(_classUnderTest.GetMaxForDepth(43), "Should be null for 43m");
        }

        [TestMethod]
        public void PlannerClass_GetNoDecompressionLimitForDepth_SelectedValuesAndEdgeCases()
        {
            Assert.AreEqual(219, _classUnderTest.GetNoDecompressionLimitForDepth(0), "Unexpected value returned for 0m");
            Assert.AreEqual(219, _classUnderTest.GetNoDecompressionLimitForDepth(1), "Unexpected value returned for 1m");
            Assert.AreEqual(219, _classUnderTest.GetNoDecompressionLimitForDepth(10), "Unexpected value returned for 10m");
            Assert.AreEqual(147, _classUnderTest.GetNoDecompressionLimitForDepth(11), "Unexpected value returned for 11m");
            Assert.AreEqual(8, _classUnderTest.GetNoDecompressionLimitForDepth(42), "Unexpected value returned for 42m");
            Assert.IsNull(_classUnderTest.GetNoDecompressionLimitForDepth(43), "Should be null for 43m");
        }

        [TestMethod]
        public void PlannerClass_GetPressureGroup_SelectedValuesAndEdgeCases()
        {
            // Depth lower than minimum on table
            Assert.AreEqual('A', _classUnderTest.GetPressureGroup(8, 10), "Unexpected value for 8,10");

            // Duration lower than minimum on table
            Assert.AreEqual('A', _classUnderTest.GetPressureGroup(10, 8), "Unexpected value for 10,8");

            // Duration AND depth lower than minimum on table
            Assert.AreEqual('A', _classUnderTest.GetPressureGroup(8, 8), "Unexpected value for 8,8");

            // Matches cell exactly
            Assert.AreEqual('A', _classUnderTest.GetPressureGroup(10, 10), "Unexpected value for 10,10");

            // Depth just over cell value
            Assert.AreEqual('B', _classUnderTest.GetPressureGroup(11, 11), "Unexpected value for 11,10");

            // Duration just over cell value
            Assert.AreEqual('B', _classUnderTest.GetPressureGroup(10, 11), "Unexpected value for 10,11");

            // Duration at maximum for depth
            Assert.AreEqual('Z', _classUnderTest.GetPressureGroup(10, 219), "Unexpected value for 10,219");

            // Duration above maximum for depth
            Assert.IsNull(_classUnderTest.GetPressureGroup(10, 220), "Unexpected value for 10,220");

            // Depth at Maximum for table
            Assert.AreEqual('B', _classUnderTest.GetPressureGroup(42, 4), "Unexpected value for 42,4");

            // Depth over Maximum for table
            Assert.IsNull(_classUnderTest.GetPressureGroup(43, 4), "Unexpected value for 43,4");

            // Duration and Depth over Maximum for table
            Assert.IsNull(_classUnderTest.GetPressureGroup(43, 9), "Unexpected value for 43,9");

            // Cell skips down to next depth
            Assert.AreEqual('B', _classUnderTest.GetPressureGroup(40, 1), "Unexpected value for 40,1");
            Assert.AreEqual('B', _classUnderTest.GetPressureGroup(42, 1), "Unexpected value for 42,1");
            Assert.AreEqual('D', _classUnderTest.GetPressureGroup(42, 5), "Unexpected value for 42,5");

            // can't test skip-down for 35 metres for 8.5 mins
            // can't test skip-down for 40 metres for 6.5 mins
        }

        [TestMethod]
        public void PlannerClass_GetTableDepth_AllValuesAndEdgeCases()
        {
            Assert.AreEqual(10, _classUnderTest.GetTableDepth(0), "Unexpected value for 0m");
            Assert.AreEqual(10, _classUnderTest.GetTableDepth(1), "Unexpected value for 1m");
            Assert.AreEqual(10, _classUnderTest.GetTableDepth(10), "Unexpected value for 10m");
            Assert.AreEqual(12, _classUnderTest.GetTableDepth(11), "Unexpected value for 11m");
            Assert.AreEqual(42, _classUnderTest.GetTableDepth(42), "Unexpected value for 42m");

            Assert.IsNull(_classUnderTest.GetTableDepth(43), "Should be null for 43m");
        }

        [TestMethod]
        public void PlannerClass_GetTimeCell_SelectedValuesAndEdgeCases()
        {
            new TimeCell(10, 10, 'A').AssertHasSamePropertyValues(_classUnderTest.GetTimeCell(0, 0));
            new TimeCell(10, 10, 'A').AssertHasSamePropertyValues(_classUnderTest.GetTimeCell(1, 1));
            new TimeCell(10, 10, 'A').AssertHasSamePropertyValues(_classUnderTest.GetTimeCell(10, 10));
            new TimeCell(12, 9, 'A').AssertHasSamePropertyValues(_classUnderTest.GetTimeCell(11, 9));
            new TimeCell(10, 20, 'B').AssertHasSamePropertyValues(_classUnderTest.GetTimeCell(10, 11));

            new TimeCell(10, 219, 'Z', false, true, true).AssertHasSamePropertyValues(_classUnderTest.GetTimeCell(10, 219));
            Assert.IsNull(_classUnderTest.GetTimeCell(10, 220), "Should be null for 10 metres for 220 mins");
            Assert.IsNull(_classUnderTest.GetTimeCell(43, 0), "Should be null for 43m for 0 mins");
            Assert.IsNull(_classUnderTest.GetTimeCell(43, 9), "Should be null for 43m for 9 mins");
        }

        [TestMethod]
        public void PlannerClass_GetPressureGroupAfterSurfaceInterval_SelectedValuesAndEdgeCases()
        {
            Assert.AreEqual('A', _classUnderTest.GetPressureGroupAfterSurfaceInterval('A', new TimeSpan(0, 0, 0)), "Unexpected pressure group for A after 00:00 minutes");
            Assert.AreEqual('A', _classUnderTest.GetPressureGroupAfterSurfaceInterval('A', new TimeSpan(0, 3, 0)), "Unexpected pressure group for A after 00:03 minutes");
            Assert.AreEqual('A', _classUnderTest.GetPressureGroupAfterSurfaceInterval('A', new TimeSpan(0, 4, 0)), "Unexpected pressure group for A after 00:04 minutes");
            Assert.AreEqual('B', _classUnderTest.GetPressureGroupAfterSurfaceInterval('B', new TimeSpan(0, 0, 0)), "Unexpected pressure group for B after 00:04 minutes");
            Assert.AreEqual('B', _classUnderTest.GetPressureGroupAfterSurfaceInterval('B', new TimeSpan(0, 47, 0)), "Unexpected pressure group for B after 00:47 minutes");
            Assert.AreEqual('A', _classUnderTest.GetPressureGroupAfterSurfaceInterval('B', new TimeSpan(0, 48, 0)), "Unexpected pressure group for B after 00:48 minutes");
            Assert.AreEqual('A', _classUnderTest.GetPressureGroupAfterSurfaceInterval('B', new TimeSpan(3, 48, 0)), "Unexpected pressure group for B after 03:48 minutes");
            Assert.AreEqual('A', _classUnderTest.GetPressureGroupAfterSurfaceInterval('B', new TimeSpan(3, 48, 0)), "Unexpected pressure group for B after 03:49 minutes");

            Assert.AreEqual('Z', _classUnderTest.GetPressureGroupAfterSurfaceInterval('Z', new TimeSpan(0, 0, 0)), "Unexpected pressure group for Z after 00:00 minutes");
            Assert.AreEqual('Z', _classUnderTest.GetPressureGroupAfterSurfaceInterval('Z', new TimeSpan(0, 2, 0)), "Unexpected pressure group for Z after 00:02 minutes");
            Assert.AreEqual('Y', _classUnderTest.GetPressureGroupAfterSurfaceInterval('Z', new TimeSpan(0, 3, 0)), "Unexpected pressure group for Z after 00:03 minutes");
            Assert.AreEqual('A', _classUnderTest.GetPressureGroupAfterSurfaceInterval('Z', new TimeSpan(6, 0, 0)), "Unexpected pressure group for Z after 06:00 minutes");
            Assert.AreEqual('A', _classUnderTest.GetPressureGroupAfterSurfaceInterval('Z', new TimeSpan(6, 0, 1)), "Unexpected pressure group for Z after 06:01 minutes");
        }

        [TestMethod]
        public void PlannerClass_GetRepetitiveDiveCell_SelectedValuesAndEdgeCases()
        {
            // below table limit
            new RepetitiveDiveCell('A', 10, 10, 209).AssertHasSamePropertyValues(_classUnderTest.GetRepetitiveDiveCell('A', 0));
            new RepetitiveDiveCell('A', 10, 10, 209).AssertHasSamePropertyValues(_classUnderTest.GetRepetitiveDiveCell('A', 1));

            // exact cell
            new RepetitiveDiveCell('A', 10, 10, 209).AssertHasSamePropertyValues(_classUnderTest.GetRepetitiveDiveCell('A', 10));

            // round up to next row
            new RepetitiveDiveCell('A', 12, 9, 138).AssertHasSamePropertyValues(_classUnderTest.GetRepetitiveDiveCell('A', 11));

            // limit of table
            new RepetitiveDiveCell('A', 40, 2, 7).AssertHasSamePropertyValues(_classUnderTest.GetRepetitiveDiveCell('A', 40));

            // null AdjustedNoDecompressionLimit

            // if working like the tables
            //new RepetitiveDiveCell('C', 40, 6, null).AssertHasSamePropertyValues(_classUnderTest.GetRepetitiveDiveCell('C', 40));

            // if working as an eRDPMl
            new RepetitiveDiveCell('C', 40, 6, 3).AssertHasSamePropertyValues(_classUnderTest.GetRepetitiveDiveCell('C', 40));

            // blank cell
            Assert.IsNull(_classUnderTest.GetRepetitiveDiveCell('H', 40), "Should be null for H @ 40m");

            // outside table limits
            Assert.IsNull(_classUnderTest.GetRepetitiveDiveCell('A', 41), "Should be null for 41m");
        }

        [TestMethod]
        public void PlannerClass_GetMinimumSurfaceInterval_SelectedValuesAndEdgeCases()
        {
            _classUnderTest.GetMinimumSurfaceInterval('A', 0, 0).AssertHasSamePropertyValues(new SurfaceInterval('A', new TimeSpan(0, 0, 0), new TimeSpan(0, 3, 0), 'A', 10, 209));

            _classUnderTest.GetMinimumSurfaceInterval('A', 40, 7).AssertHasSamePropertyValues(new SurfaceInterval('A', new TimeSpan(0, 0, 0), new TimeSpan(0, 3, 0), 'A', 2, 7));

            // can't do this dive. Max mins = 6
            Assert.IsNull(_classUnderTest.GetMinimumSurfaceInterval('C', 40, 8), "Unexpected minimum surface interval");

            // M -> J after 15 mins allowing for 20 mins @ 20 metres
            _classUnderTest.GetMinimumSurfaceInterval('M', 20, 20).AssertHasSamePropertyValues(new SurfaceInterval('M', new TimeSpan(0, 15, 0), new TimeSpan(0, 19, 0), 'J', 25, 20));

            // Surface interval out of range of table. Should return an 'A'
            _classUnderTest.GetMinimumSurfaceInterval('Z', 10, 20, new TimeSpan(7, 0, 0)).AssertHasSamePropertyValues(new SurfaceInterval('Z', new TimeSpan(3, 0, 0), new TimeSpan(6, 0, 0), 'A', 10, 209));
        }

        [TestMethod]
        public void PlannerClass_MinimumSurfaceInterval_AllValues()
        {
            TimeSpan zeroHours = new TimeSpan(0, 0, 0);
            TimeSpan oneHour = new TimeSpan(1, 0, 0);
            TimeSpan threeHours = new TimeSpan(3, 0, 0);

            Assert.AreEqual(zeroHours, PlannerClass.AdjustMinimumSurfaceIntervalForRuleWxyz('A'), "Unexpected value for 'A'");
            Assert.AreEqual(zeroHours, PlannerClass.AdjustMinimumSurfaceIntervalForRuleWxyz('1'), "Unexpected value for '1'");
            Assert.AreEqual(zeroHours, PlannerClass.AdjustMinimumSurfaceIntervalForRuleWxyz(' '), "Unexpected value for ' '");
            Assert.AreEqual(zeroHours, PlannerClass.AdjustMinimumSurfaceIntervalForRuleWxyz('_'), "Unexpected value for '_'");

            Assert.AreEqual(oneHour, PlannerClass.AdjustMinimumSurfaceIntervalForRuleWxyz('W'), "Unexpected value for 'W'");
            Assert.AreEqual(oneHour, PlannerClass.AdjustMinimumSurfaceIntervalForRuleWxyz('X'), "Unexpected value for 'X'");
            Assert.AreEqual(threeHours, PlannerClass.AdjustMinimumSurfaceIntervalForRuleWxyz('Y'), "Unexpected value for 'Y'");
            Assert.AreEqual(threeHours, PlannerClass.AdjustMinimumSurfaceIntervalForRuleWxyz('Z'), "Unexpected value for 'Z'");
        }

        [TestMethod]
        public void PlannerClass_CalculateDivePlan_SelectedValuesAndEdgeCases()
        {
            // no dives
            Assert.IsNull(_classUnderTest.CalculateDivePlan(new List<Dive>()), "Unexpected dive plans returned");

            // single dive
            List<Dive> divePlan1 = _classUnderTest.CalculateDivePlan(new List<Dive>
                                                                         {
                                                                             new Dive(10, 20)
                                                                                 {
                                                                                     PressureGroupBeforeDive = 'A'
                                                                                 }
                                                                         });

            Assert.AreEqual(1, divePlan1.Count, "Unexpected number of plans returned");
            divePlan1[0].AssertHasSamePropertyValues(new Dive(10, 20)
                                                         {
                                                             AdjustedNoDecompressionLimitBroughtForward = 0,
                                                             AdjustedNoDecompressionLimitToCarryForward = 0,
                                                             PressureGroupAfterDive = 'B',
                                                             PressureGroupAfterSurfaceInterval = null,
                                                             PressureGroupBeforeDive = 'A',
                                                             ResidualNitrogenTimeBroughtForward = 0,
                                                             ResidualNitrogenTimeToCarryForward = 0,
                                                             SafetyStop = false,
                                                             SurfaceInterval = null,
                                                             TotalBottomTime = 20
                                                         });

            // two dives
            List<Dive> divePlan2 = _classUnderTest.CalculateDivePlan(new List<Dive>
                                                                         {
                                                                             new Dive(20, 21),
                                                                             new Dive(30, 10)
                                                                         });

            Assert.AreEqual(2, divePlan2.Count, "Unexpected number of plans returned");
            divePlan2[0].AssertHasSamePropertyValues(new Dive(20, 21)
                                                         {
                                                             AdjustedNoDecompressionLimitBroughtForward = 0,
                                                             AdjustedNoDecompressionLimitToCarryForward = 10,
                                                             PressureGroupAfterDive = 'H',
                                                             PressureGroupAfterSurfaceInterval = 'E',
                                                             PressureGroupBeforeDive = 'A',
                                                             ResidualNitrogenTimeBroughtForward = 0,
                                                             ResidualNitrogenTimeToCarryForward = 10,
                                                             SafetyStop = false,
                                                             SurfaceInterval = new TimeSpan(0, 21, 0),
                                                             TotalBottomTime = 21
                                                         });

            divePlan2[1].AssertHasSamePropertyValues(new Dive(30, 10)
                                                         {
                                                             AdjustedNoDecompressionLimitBroughtForward = 10,
                                                             AdjustedNoDecompressionLimitToCarryForward = 0,
                                                             PressureGroupAfterDive = 'N',
                                                             PressureGroupAfterSurfaceInterval = null,
                                                             PressureGroupBeforeDive = 'E',
                                                             ResidualNitrogenTimeBroughtForward = 10,
                                                             ResidualNitrogenTimeToCarryForward = 0,
                                                             SafetyStop = true,
                                                             SurfaceInterval = null,
                                                             TotalBottomTime = 20
                                                         });

            // three dives - no surface intervals
            List<Dive> divePlan3 = _classUnderTest.CalculateDivePlan(new List<Dive>
                                                                         {
                                                                             new Dive(20, 30),
                                                                             new Dive(15, 25),
                                                                             new Dive(10, 20)
                                                                         });

            Assert.AreEqual(3, divePlan3.Count, "Unexpected number of plans returned");
            divePlan3[0].AssertHasSamePropertyValues(new Dive(20, 30)
                                                         {
                                                             ResidualNitrogenTimeBroughtForward = 0,
                                                             AdjustedNoDecompressionLimitBroughtForward = 0,
                                                             PressureGroupBeforeDive = 'A',
                                                             TotalBottomTime = 30,
                                                             SafetyStop = false,
                                                             PressureGroupAfterDive = 'M',
                                                             SurfaceInterval = new TimeSpan(0, 0, 0),
                                                             PressureGroupAfterSurfaceInterval = 'M',
                                                             ResidualNitrogenTimeToCarryForward = 39,
                                                             AdjustedNoDecompressionLimitToCarryForward = 33
                                                         });

            divePlan3[1].AssertHasSamePropertyValues(new Dive(15, 25)
                                                         {
                                                             ResidualNitrogenTimeBroughtForward = 39,
                                                             AdjustedNoDecompressionLimitBroughtForward = 33,
                                                             PressureGroupBeforeDive = 'M',
                                                             TotalBottomTime = 64,
                                                             SafetyStop = true,
                                                             PressureGroupAfterDive = 'V',
                                                             SurfaceInterval = new TimeSpan(0, 0, 0),
                                                             PressureGroupAfterSurfaceInterval = 'V',
                                                             ResidualNitrogenTimeToCarryForward = 145,
                                                             AdjustedNoDecompressionLimitToCarryForward = 74
                                                         });
            divePlan3[2].AssertHasSamePropertyValues(new Dive(10, 20)
                                                         {
                                                             ResidualNitrogenTimeBroughtForward = 145,
                                                             AdjustedNoDecompressionLimitBroughtForward = 74,
                                                             PressureGroupBeforeDive = 'V',
                                                             TotalBottomTime = 165,
                                                             SafetyStop = true,
                                                             PressureGroupAfterDive = 'X',
                                                             SurfaceInterval = null,
                                                             PressureGroupAfterSurfaceInterval = null,
                                                             ResidualNitrogenTimeToCarryForward = 0,
                                                             AdjustedNoDecompressionLimitToCarryForward = 0
                                                         });

            // three dives - requested 1 hour between dives
            List<Dive> divePlan4 = _classUnderTest.CalculateDivePlan(new List<Dive>
                                                                         {
                                                                             new Dive(20, 30, new TimeSpan(1, 0, 0)),
                                                                             new Dive(15, 25, new TimeSpan(1, 0, 0)),
                                                                             new Dive(10, 20)
                                                                         });

            Assert.AreEqual(3, divePlan4.Count, "Unexpected number of plans returned");
            divePlan4[0].AssertHasSamePropertyValues(new Dive(20, 30, new TimeSpan(1, 0, 0))
                                                         {
                                                             ResidualNitrogenTimeBroughtForward = 0,
                                                             AdjustedNoDecompressionLimitBroughtForward = 0,
                                                             PressureGroupBeforeDive = 'A',
                                                             TotalBottomTime = 30,
                                                             SafetyStop = false,
                                                             PressureGroupAfterDive = 'M',
                                                             SurfaceInterval = new TimeSpan(0, 56, 0),
                                                             PressureGroupAfterSurfaceInterval = 'D',
                                                             ResidualNitrogenTimeToCarryForward = 19,
                                                             AdjustedNoDecompressionLimitToCarryForward = 53
                                                         });

            divePlan4[1].AssertHasSamePropertyValues(new Dive(15, 25, new TimeSpan(1, 0, 0))
                                                         {
                                                             ResidualNitrogenTimeBroughtForward = 19,
                                                             AdjustedNoDecompressionLimitBroughtForward = 53,
                                                             PressureGroupBeforeDive = 'D',
                                                             TotalBottomTime = 44,
                                                             SafetyStop = false,
                                                             PressureGroupAfterDive = 'O',
                                                             SurfaceInterval = new TimeSpan(0, 56, 0),
                                                             PressureGroupAfterSurfaceInterval = 'E',
                                                             ResidualNitrogenTimeToCarryForward = 34,
                                                             AdjustedNoDecompressionLimitToCarryForward = 185
                                                         });
            divePlan4[2].AssertHasSamePropertyValues(new Dive(10, 20, new TimeSpan(1, 0, 0))
                                                         {
                                                             ResidualNitrogenTimeBroughtForward = 34,
                                                             AdjustedNoDecompressionLimitBroughtForward = 185,
                                                             PressureGroupBeforeDive = 'E',
                                                             TotalBottomTime = 54,
                                                             SafetyStop = false,
                                                             PressureGroupAfterDive = 'J',
                                                             SurfaceInterval = null,
                                                             PressureGroupAfterSurfaceInterval = null,
                                                             ResidualNitrogenTimeToCarryForward = 0,
                                                             AdjustedNoDecompressionLimitToCarryForward = 0
                                                         });
        }
    }
}