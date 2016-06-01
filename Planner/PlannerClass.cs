using System;
using System.Collections.Generic;
using System.Linq;

namespace Planner
{
    public class PlannerClass
    {
        private const char MinPressureGroup = 'A';
        private static PlannerClass _instance;
        private List<RepetitiveDiveCell> _repetitiveDiveCells;
        private List<SurfaceIntervalCreditCell> _surfaceIntervalCreditCells;
        private List<TimeCell> _timeCells;
        /*  
            RDP Notes
            
            Safety Stops
            A safety stop for 3 minutes at 5m is required any time the diver comes up to or within 3 pressure groups of a
            no decompression limit and for any dive to a depth of 30m or deeper.

            Emergency Decompression
            If a no decompression limit is exceeded by no more than 5 minutes, an 8 minute decompression stop at 5m is mandatory.
            Upon surfacing, the diver must remain out of the water for at least 6 hours prior to making another dive.
            If a no decompression limit is exceeded by more than 5 minutes, a 5m decompression stop of no less than 15 minutes is urged
            (air supply permitting). Upon surfacing, the diver must remain out of the water for at least 24 hours prior to making another dive.

            Flying After Diving Recommendations
            For Dives Within the No Decompression Limits
            • Single Dives: A minimum pre-flight surface interval of 12 hours is suggested.
            • Repetitive Dives and/or Multi-day Dives: A minimum pre-flight surface interval of 18 hours is suggested.
            
            For Dives Requiring Decompression Stops
            • A minimum pre-flight surface interval greater than 18 hours is suggested.

            Diving at Altitude
            Diving at altitude (300m or higher) requires the use of special procedures.

            Special Rules for Multiple Dives
            If you are planning 3 or more dives in a day: Beginning with the first dive, if your ending pressure group after any dive is W or X,
            the minimum surface interval between all subsequent dives is 1 hour. If your ending pressure group after any dive is Y or Z, the
            minimum surface interval between all subsequent dives is 3 hours.
            
            Note: Since little is presently known about the physiological effects of multiple dives over multiple days, divers are wise to make
            fewer dives and limit their exposure toward the end of a multi-day dive series.
            
            General Rules
            • Ascend from all dives at a rate not to exceed 18m per minute.
            • When planning a dive in cold water or under conditions that might be strenuous,
              plan the dive assuming the depth is 4m deeper than actual.
            • Plan repetitive dives so each successive dive is to a shallower depth. Limit repetitive dives to 30m or shallower.
            • Never exceed the limits of this planner and, whenever possible, avoid diving to the limits of the planner.
              42m is for emergency purposes only, do not dive to this depth.
         */

        /// <summary>Prevents a default instance of the <see cref="PlannerClass"/> class from being created.</summary>
        private PlannerClass()
        {
            InitialiseDataStructures();
        }

        public static PlannerClass Instance
        {
            get { return _instance ?? (_instance = new PlannerClass()); }
        }

        private void InitialiseDataStructures()
        {
            #region INITIALISE TIME CELLS

            _timeCells = new List<TimeCell>
                             {
                                 // 10 metres
                                 new TimeCell(10, 10, 'A'),
                                 new TimeCell(10, 20, 'B'),
                                 new TimeCell(10, 26, 'C'),
                                 new TimeCell(10, 30, 'D'),
                                 new TimeCell(10, 34, 'E'),
                                 new TimeCell(10, 37, 'F'),
                                 new TimeCell(10, 41, 'G'),
                                 new TimeCell(10, 45, 'H'),
                                 new TimeCell(10, 50, 'I'),
                                 new TimeCell(10, 54, 'J'),
                                 new TimeCell(10, 59, 'K'),
                                 new TimeCell(10, 64, 'L'),
                                 new TimeCell(10, 70, 'M'),
                                 new TimeCell(10, 75, 'N'),
                                 new TimeCell(10, 82, 'O'),
                                 new TimeCell(10, 88, 'P'),
                                 new TimeCell(10, 95, 'Q'),
                                 new TimeCell(10, 104, 'R'),
                                 new TimeCell(10, 112, 'S'),
                                 new TimeCell(10, 122, 'T'),
                                 new TimeCell(10, 133, 'U'),
                                 new TimeCell(10, 145, 'V'),
                                 new TimeCell(10, 160, 'W', safetyStopRequired: true),
                                 new TimeCell(10, 178, 'X', safetyStopRequired: true),
                                 new TimeCell(10, 199, 'Y', safetyStopRequired: true),
                                 new TimeCell(10, 219, 'Z', safetyStopRequired: true, noDecompressionLimit: true),

                                 // 12 metres
                                 new TimeCell(12, 9, 'A'),
                                 new TimeCell(12, 17, 'B'),
                                 new TimeCell(12, 23, 'C'),
                                 new TimeCell(12, 26, 'D'),
                                 new TimeCell(12, 29, 'E'),
                                 new TimeCell(12, 32, 'F'),
                                 new TimeCell(12, 35, 'G'),
                                 new TimeCell(12, 38, 'H'),
                                 new TimeCell(12, 42, 'I'),
                                 new TimeCell(12, 45, 'J'),
                                 new TimeCell(12, 49, 'K'),
                                 new TimeCell(12, 53, 'L'),
                                 new TimeCell(12, 57, 'M'),
                                 new TimeCell(12, 62, 'N'),
                                 new TimeCell(12, 66, 'O'),
                                 new TimeCell(12, 71, 'P'),
                                 new TimeCell(12, 76, 'Q'),
                                 new TimeCell(12, 82, 'R'),
                                 new TimeCell(12, 88, 'S'),
                                 new TimeCell(12, 94, 'T'),
                                 new TimeCell(12, 101, 'U'),
                                 new TimeCell(12, 108, 'V'),
                                 new TimeCell(12, 116, 'W', safetyStopRequired: true),
                                 new TimeCell(12, 125, 'X', safetyStopRequired: true),
                                 new TimeCell(12, 134, 'Y', safetyStopRequired: true),
                                 new TimeCell(12, 147, 'Z', safetyStopRequired: true, noDecompressionLimit: true),

                                 // 14 metres
                                 new TimeCell(14, 8, 'A'),
                                 new TimeCell(14, 15, 'B'),
                                 new TimeCell(14, 19, 'C'),
                                 new TimeCell(14, 22, 'D'),
                                 new TimeCell(14, 24, 'E'),
                                 new TimeCell(14, 27, 'F'),
                                 new TimeCell(14, 29, 'G'),
                                 new TimeCell(14, 32, 'H'),
                                 new TimeCell(14, 35, 'I'),
                                 new TimeCell(14, 37, 'J'),
                                 new TimeCell(14, 40, 'K'),
                                 new TimeCell(14, 43, 'L'),
                                 new TimeCell(14, 47, 'M'),
                                 new TimeCell(14, 50, 'N'),
                                 new TimeCell(14, 53, 'O'),
                                 new TimeCell(14, 57, 'P'),
                                 new TimeCell(14, 61, 'Q'),
                                 new TimeCell(14, 64, 'R'),
                                 new TimeCell(14, 68, 'S'),
                                 new TimeCell(14, 73, 'T'),
                                 new TimeCell(14, 77, 'U'),
                                 new TimeCell(14, 82, 'V', safetyStopRequired: true),
                                 new TimeCell(14, 87, 'W', safetyStopRequired: true),
                                 new TimeCell(14, 92, 'X', safetyStopRequired: true),
                                 new TimeCell(14, 98, 'Y', safetyStopRequired: true, noDecompressionLimit: true),

                                 // 16 metres
                                 new TimeCell(16, 7, 'A'),
                                 new TimeCell(16, 13, 'B'),
                                 new TimeCell(16, 17, 'C'),
                                 new TimeCell(16, 19, 'D'),
                                 new TimeCell(16, 21, 'E'),
                                 new TimeCell(16, 23, 'F'),
                                 new TimeCell(16, 25, 'G'),
                                 new TimeCell(16, 27, 'H'),
                                 new TimeCell(16, 29, 'I'),
                                 new TimeCell(16, 32, 'J'),
                                 new TimeCell(16, 34, 'K'),
                                 new TimeCell(16, 37, 'L'),
                                 new TimeCell(16, 39, 'M'),
                                 new TimeCell(16, 42, 'N'),
                                 new TimeCell(16, 45, 'O'),
                                 new TimeCell(16, 48, 'P'),
                                 new TimeCell(16, 50, 'Q'),
                                 new TimeCell(16, 53, 'R'),
                                 new TimeCell(16, 56, 'S'),
                                 new TimeCell(16, 60, 'T'),
                                 new TimeCell(16, 63, 'U', safetyStopRequired: true),
                                 new TimeCell(16, 67, 'V', safetyStopRequired: true),
                                 new TimeCell(16, 70, 'W', safetyStopRequired: true),
                                 new TimeCell(16, 72, 'X', safetyStopRequired: true, noDecompressionLimit: true),

                                 // 18 metres
                                 new TimeCell(18, 6, 'A'),
                                 new TimeCell(18, 11, 'B'),
                                 new TimeCell(18, 15, 'C'),
                                 new TimeCell(18, 16, 'D'),
                                 new TimeCell(18, 18, 'E'),
                                 new TimeCell(18, 20, 'F'),
                                 new TimeCell(18, 22, 'G'),
                                 new TimeCell(18, 24, 'H'),
                                 new TimeCell(18, 26, 'I'),
                                 new TimeCell(18, 28, 'J'),
                                 new TimeCell(18, 30, 'K'),
                                 new TimeCell(18, 32, 'L'),
                                 new TimeCell(18, 34, 'M'),
                                 new TimeCell(18, 36, 'N'),
                                 new TimeCell(18, 39, 'O'),
                                 new TimeCell(18, 41, 'P'),
                                 new TimeCell(18, 43, 'Q'),
                                 new TimeCell(18, 46, 'R'),
                                 new TimeCell(18, 48, 'S'),
                                 new TimeCell(18, 51, 'T', safetyStopRequired: true),
                                 new TimeCell(18, 53, 'U', safetyStopRequired: true),
                                 new TimeCell(18, 55, 'V', safetyStopRequired: true),
                                 new TimeCell(18, 56, 'W', safetyStopRequired: true, noDecompressionLimit: true),

                                 // 20 metres
                                 new TimeCell(20, 6, 'A'),
                                 new TimeCell(20, 10, 'B'),
                                 new TimeCell(20, 13, 'C'),
                                 new TimeCell(20, 15, 'D'),
                                 new TimeCell(20, 16, 'E'),
                                 new TimeCell(20, 18, 'F'),
                                 new TimeCell(20, 20, 'G'),
                                 new TimeCell(20, 21, 'H'),
                                 new TimeCell(20, 23, 'I'),
                                 new TimeCell(20, 25, 'J'),
                                 new TimeCell(20, 26, 'K'),
                                 new TimeCell(20, 28, 'L'),
                                 new TimeCell(20, 30, 'M'),
                                 new TimeCell(20, 32, 'N'),
                                 new TimeCell(20, 34, 'O'),
                                 new TimeCell(20, 36, 'P'),
                                 new TimeCell(20, 38, 'Q'),
                                 new TimeCell(20, 40, 'R', safetyStopRequired: true),
                                 new TimeCell(20, 42, 'S', safetyStopRequired: true),
                                 new TimeCell(20, 44, 'T', safetyStopRequired: true),
                                 new TimeCell(20, 45, 'U', safetyStopRequired: true, noDecompressionLimit: true),

                                 // 22 metres
                                 new TimeCell(22, 5, 'A'),
                                 new TimeCell(22, 9, 'B'),
                                 new TimeCell(22, 12, 'C'),
                                 new TimeCell(22, 13, 'D'),
                                 new TimeCell(22, 15, 'E'),
                                 new TimeCell(22, 16, 'F'),
                                 new TimeCell(22, 18, 'G'),
                                 new TimeCell(22, 19, 'H'),
                                 new TimeCell(22, 21, 'I'),
                                 new TimeCell(22, 22, 'J'),
                                 new TimeCell(22, 24, 'K'),
                                 new TimeCell(22, 25, 'L'),
                                 new TimeCell(22, 27, 'M'),
                                 new TimeCell(22, 29, 'N'),
                                 new TimeCell(22, 30, 'O'),
                                 new TimeCell(22, 32, 'P', safetyStopRequired: true),
                                 new TimeCell(22, 34, 'Q', safetyStopRequired: true),
                                 new TimeCell(22, 36, 'R', safetyStopRequired: true),
                                 new TimeCell(22, 37, 'S', safetyStopRequired: true, noDecompressionLimit: true),

                                 // 25 metres
                                 new TimeCell(25, 4, 'A'),
                                 new TimeCell(25, 8, 'B'),
                                 new TimeCell(25, 10, 'C'),
                                 new TimeCell(25, 11, 'D'),
                                 new TimeCell(25, 13, 'E'),
                                 new TimeCell(25, 14, 'F'),
                                 new TimeCell(25, 15, 'G'),
                                 new TimeCell(25, 17, 'H'),
                                 new TimeCell(25, 18, 'I'),
                                 new TimeCell(25, 19, 'J'),
                                 new TimeCell(25, 21, 'K'),
                                 new TimeCell(25, 22, 'L'),
                                 new TimeCell(25, 23, 'M'),
                                 new TimeCell(25, 25, 'N', safetyStopRequired: true),
                                 new TimeCell(25, 26, 'O', safetyStopRequired: true),
                                 new TimeCell(25, 28, 'P', safetyStopRequired: true),
                                 new TimeCell(25, 29, 'Q', safetyStopRequired: true, noDecompressionLimit: true),

                                 // 30 metres
                                 new TimeCell(30, 3, 'A', safetyStopRequired: true),
                                 new TimeCell(30, 6, 'B', safetyStopRequired: true),
                                 new TimeCell(30, 8, 'C', safetyStopRequired: true),
                                 new TimeCell(30, 9, 'D', safetyStopRequired: true),
                                 new TimeCell(30, 10, 'E', safetyStopRequired: true),
                                 new TimeCell(30, 11, 'F', safetyStopRequired: true),
                                 new TimeCell(30, 12, 'G', safetyStopRequired: true),
                                 new TimeCell(30, 13, 'H', safetyStopRequired: true),
                                 new TimeCell(30, 14, 'I', safetyStopRequired: true),
                                 new TimeCell(30, 15, 'J', safetyStopRequired: true),
                                 new TimeCell(30, 16, 'K', safetyStopRequired: true),
                                 new TimeCell(30, 17, 'L', safetyStopRequired: true),
                                 new TimeCell(30, 19, 'M', safetyStopRequired: true),
                                 new TimeCell(30, 20, 'N', safetyStopRequired: true, noDecompressionLimit: true),

                                 // 35 metres
                                 new TimeCell(35, 3, 'A', safetyStopRequired: true),
                                 new TimeCell(35, 5, 'B', safetyStopRequired: true),
                                 new TimeCell(35, 7, 'C', safetyStopRequired: true),
                                 new TimeCell(35, 8, 'D', safetyStopRequired: true),
                                 new TimeCell(35, null, 'E', safetyStopRequired: true, advanceToNextDuration: true),
                                 new TimeCell(35, 9, 'F', safetyStopRequired: true),
                                 new TimeCell(35, 10, 'G', safetyStopRequired: true),
                                 new TimeCell(35, 11, 'H', safetyStopRequired: true),
                                 new TimeCell(35, 12, 'I', safetyStopRequired: true),
                                 new TimeCell(35, 13, 'J', safetyStopRequired: true),
                                 new TimeCell(35, 14, 'K', safetyStopRequired: true, noDecompressionLimit: true),

                                 // 40 metres
                                 new TimeCell(40, null, 'A', safetyStopRequired: true, advanceToNextDuration: true),
                                 new TimeCell(40, 5, 'B', safetyStopRequired: true),
                                 new TimeCell(40, 6, 'C', safetyStopRequired: true),
                                 new TimeCell(40, null, 'D', safetyStopRequired: true, advanceToNextDuration: true),
                                 new TimeCell(40, 7, 'E', safetyStopRequired: true),
                                 new TimeCell(40, 8, 'F', safetyStopRequired: true),
                                 new TimeCell(40, 9, 'G', safetyStopRequired: true, noDecompressionLimit: true),

                                 // 42 metres
                                 new TimeCell(42, null, 'A', safetyStopRequired: true, advanceToNextDuration: true),
                                 new TimeCell(42, 4, 'B', safetyStopRequired: true),
                                 new TimeCell(42, null, 'C', safetyStopRequired: true, advanceToNextDuration: true),
                                 new TimeCell(42, 6, 'D', safetyStopRequired: true),
                                 new TimeCell(42, 7, 'E', safetyStopRequired: true),
                                 new TimeCell(42, 8, 'F', safetyStopRequired: true, noDecompressionLimit: true)
                             };

            #endregion

            #region INITIALISE SURFACE INTERVAL CREDIT CELLS

            _surfaceIntervalCreditCells = new List<SurfaceIntervalCreditCell>
                                              {
                                                  // A
                                                  new SurfaceIntervalCreditCell('A', "00:00", "00:03", 'A'),

                                                  // B
                                                  new SurfaceIntervalCreditCell('B', "00:00", "00:47", 'B'),
                                                  new SurfaceIntervalCreditCell('B', "00:48", "03:48", 'A'),

                                                  // C
                                                  new SurfaceIntervalCreditCell('C', "00:00", "00:21", 'C'),
                                                  new SurfaceIntervalCreditCell('C', "00:22", "01:09", 'B'),
                                                  new SurfaceIntervalCreditCell('C', "01:10", "04:10", 'A'),

                                                  // D
                                                  new SurfaceIntervalCreditCell('D', "00:00", "00:08", 'D'),
                                                  new SurfaceIntervalCreditCell('D', "00:09", "00:30", 'C'),
                                                  new SurfaceIntervalCreditCell('D', "00:31", "01:18", 'B'),
                                                  new SurfaceIntervalCreditCell('D', "01:19", "04:19", 'A'),

                                                  // E
                                                  new SurfaceIntervalCreditCell('E', "00:00", "00:07", 'E'),
                                                  new SurfaceIntervalCreditCell('E', "00:08", "00:16", 'D'),
                                                  new SurfaceIntervalCreditCell('E', "00:17", "00:38", 'C'),
                                                  new SurfaceIntervalCreditCell('E', "00:39", "01:27", 'B'),
                                                  new SurfaceIntervalCreditCell('E', "01:28", "04:28", 'A'),

                                                  // F
                                                  new SurfaceIntervalCreditCell('F', "00:00", "00:07", 'F'),
                                                  new SurfaceIntervalCreditCell('F', "00:08", "00:15", 'E'),
                                                  new SurfaceIntervalCreditCell('F', "00:16", "00:24", 'D'),
                                                  new SurfaceIntervalCreditCell('F', "00:25", "00:46", 'C'),
                                                  new SurfaceIntervalCreditCell('F', "00:47", "01:34", 'B'),
                                                  new SurfaceIntervalCreditCell('F', "01:35", "04:35", 'A'),

                                                  // G
                                                  new SurfaceIntervalCreditCell('G', "00:00", "00:06", 'G'),
                                                  new SurfaceIntervalCreditCell('G', "00:07", "00:13", 'F'),
                                                  new SurfaceIntervalCreditCell('G', "00:14", "00:22", 'E'),
                                                  new SurfaceIntervalCreditCell('G', "00:23", "00:31", 'D'),
                                                  new SurfaceIntervalCreditCell('G', "00:32", "00:53", 'C'),
                                                  new SurfaceIntervalCreditCell('G', "00:54", "01:41", 'B'),
                                                  new SurfaceIntervalCreditCell('G', "01:42", "04:42", 'A'),

                                                  // H
                                                  new SurfaceIntervalCreditCell('H', "00:00", "00:05", 'H'),
                                                  new SurfaceIntervalCreditCell('H', "00:06", "00:12", 'G'),
                                                  new SurfaceIntervalCreditCell('H', "00:13", "00:20", 'F'),
                                                  new SurfaceIntervalCreditCell('H', "00:21", "00:28", 'E'),
                                                  new SurfaceIntervalCreditCell('H', "00:29", "00:37", 'D'),
                                                  new SurfaceIntervalCreditCell('H', "00:38", "00:59", 'C'),
                                                  new SurfaceIntervalCreditCell('H', "01:00", "01:47", 'B'),
                                                  new SurfaceIntervalCreditCell('H', "01:48", "04:48", 'A'),

                                                  // I
                                                  new SurfaceIntervalCreditCell('I', "00:00", "00:05", 'I'),
                                                  new SurfaceIntervalCreditCell('I', "00:06", "00:11", 'H'),
                                                  new SurfaceIntervalCreditCell('I', "00:12", "00:18", 'G'),
                                                  new SurfaceIntervalCreditCell('I', "00:19", "00:26", 'F'),
                                                  new SurfaceIntervalCreditCell('I', "00:27", "00:34", 'E'),
                                                  new SurfaceIntervalCreditCell('I', "00:35", "00:43", 'D'),
                                                  new SurfaceIntervalCreditCell('I', "00:44", "01:05", 'C'),
                                                  new SurfaceIntervalCreditCell('I', "01:06", "01:53", 'B'),
                                                  new SurfaceIntervalCreditCell('I', "01:54", "04:54", 'A'),

                                                  // J
                                                  new SurfaceIntervalCreditCell('J', "00:00", "00:05", 'J'),
                                                  new SurfaceIntervalCreditCell('J', "00:06", "00:11", 'I'),
                                                  new SurfaceIntervalCreditCell('J', "00:12", "00:17", 'H'),
                                                  new SurfaceIntervalCreditCell('J', "00:18", "00:24", 'G'),
                                                  new SurfaceIntervalCreditCell('J', "00:25", "00:31", 'F'),
                                                  new SurfaceIntervalCreditCell('J', "00:32", "00:40", 'E'),
                                                  new SurfaceIntervalCreditCell('J', "00:41", "00:49", 'D'),
                                                  new SurfaceIntervalCreditCell('J', "00:50", "01:11", 'C'),
                                                  new SurfaceIntervalCreditCell('J', "01:12", "01:59", 'B'),
                                                  new SurfaceIntervalCreditCell('J', "02:00", "05:00", 'A'),

                                                  // K
                                                  new SurfaceIntervalCreditCell('K', "00:00", "00:04", 'K'),
                                                  new SurfaceIntervalCreditCell('K', "00:05", "00:10", 'J'),
                                                  new SurfaceIntervalCreditCell('K', "00:11", "00:16", 'I'),
                                                  new SurfaceIntervalCreditCell('K', "00:17", "00:22", 'H'),
                                                  new SurfaceIntervalCreditCell('K', "00:23", "00:29", 'G'),
                                                  new SurfaceIntervalCreditCell('K', "00:30", "00:37", 'F'),
                                                  new SurfaceIntervalCreditCell('K', "00:38", "00:45", 'E'),
                                                  new SurfaceIntervalCreditCell('K', "00:46", "00:54", 'D'),
                                                  new SurfaceIntervalCreditCell('K', "00:55", "01:16", 'C'),
                                                  new SurfaceIntervalCreditCell('K', "01:17", "02:04", 'B'),
                                                  new SurfaceIntervalCreditCell('K', "02:05", "05:05", 'A'),

                                                  // L
                                                  new SurfaceIntervalCreditCell('L', "00:00", "00:04", 'L'),
                                                  new SurfaceIntervalCreditCell('L', "00:05", "00:09", 'K'),
                                                  new SurfaceIntervalCreditCell('L', "00:10", "00:15", 'J'),
                                                  new SurfaceIntervalCreditCell('L', "00:16", "00:21", 'I'),
                                                  new SurfaceIntervalCreditCell('L', "00:22", "00:27", 'H'),
                                                  new SurfaceIntervalCreditCell('L', "00:28", "00:34", 'G'),
                                                  new SurfaceIntervalCreditCell('L', "00:35", "00:42", 'F'),
                                                  new SurfaceIntervalCreditCell('L', "00:43", "00:50", 'E'),
                                                  new SurfaceIntervalCreditCell('L', "00:51", "00:59", 'D'),
                                                  new SurfaceIntervalCreditCell('L', "01:00", "01:21", 'C'),
                                                  new SurfaceIntervalCreditCell('L', "01:22", "02:09", 'B'),
                                                  new SurfaceIntervalCreditCell('L', "02:10", "05:10", 'A'),

                                                  // M
                                                  new SurfaceIntervalCreditCell('M', "00:00", "00:04", 'M'),
                                                  new SurfaceIntervalCreditCell('M', "00:05", "00:09", 'L'),
                                                  new SurfaceIntervalCreditCell('M', "00:10", "00:14", 'K'),
                                                  new SurfaceIntervalCreditCell('M', "00:15", "00:19", 'J'),
                                                  new SurfaceIntervalCreditCell('M', "00:20", "00:25", 'I'),
                                                  new SurfaceIntervalCreditCell('M', "00:26", "00:32", 'H'),
                                                  new SurfaceIntervalCreditCell('M', "00:33", "00:39", 'G'),
                                                  new SurfaceIntervalCreditCell('M', "00:40", "00:46", 'F'),
                                                  new SurfaceIntervalCreditCell('M', "00:47", "00:55", 'E'),
                                                  new SurfaceIntervalCreditCell('M', "00:56", "01:04", 'D'),
                                                  new SurfaceIntervalCreditCell('M', "01:05", "01:25", 'C'),
                                                  new SurfaceIntervalCreditCell('M', "01:26", "02:14", 'B'),
                                                  new SurfaceIntervalCreditCell('M', "02:15", "05:15", 'A'),

                                                  // N
                                                  new SurfaceIntervalCreditCell('N', "00:00", "00:03", 'N'),
                                                  new SurfaceIntervalCreditCell('N', "00:04", "00:08", 'M'),
                                                  new SurfaceIntervalCreditCell('N', "00:09", "00:13", 'L'),
                                                  new SurfaceIntervalCreditCell('N', "00:14", "00:18", 'K'),
                                                  new SurfaceIntervalCreditCell('N', "00:19", "00:24", 'J'),
                                                  new SurfaceIntervalCreditCell('N', "00:25", "00:30", 'I'),
                                                  new SurfaceIntervalCreditCell('N', "00:31", "00:36", 'H'),
                                                  new SurfaceIntervalCreditCell('N', "00:37", "00:43", 'G'),
                                                  new SurfaceIntervalCreditCell('N', "00:44", "00:51", 'F'),
                                                  new SurfaceIntervalCreditCell('N', "00:52", "00:59", 'E'),
                                                  new SurfaceIntervalCreditCell('N', "01:00", "01:08", 'D'),
                                                  new SurfaceIntervalCreditCell('N', "01:09", "01:30", 'C'),
                                                  new SurfaceIntervalCreditCell('N', "01:31", "02:18", 'B'),
                                                  new SurfaceIntervalCreditCell('N', "02:19", "05:19", 'A'),

                                                  // O
                                                  new SurfaceIntervalCreditCell('O', "00:00", "00:03", 'O'),
                                                  new SurfaceIntervalCreditCell('O', "00:04", "00:08", 'N'),
                                                  new SurfaceIntervalCreditCell('O', "00:09", "00:12", 'M'),
                                                  new SurfaceIntervalCreditCell('O', "00:13", "00:17", 'L'),
                                                  new SurfaceIntervalCreditCell('O', "00:18", "00:23", 'K'),
                                                  new SurfaceIntervalCreditCell('O', "00:24", "00:28", 'J'),
                                                  new SurfaceIntervalCreditCell('O', "00:29", "00:34", 'I'),
                                                  new SurfaceIntervalCreditCell('O', "00:35", "00:41", 'H'),
                                                  new SurfaceIntervalCreditCell('O', "00:42", "00:47", 'G'),
                                                  new SurfaceIntervalCreditCell('O', "00:48", "00:55", 'F'),
                                                  new SurfaceIntervalCreditCell('O', "00:56", "01:03", 'E'),
                                                  new SurfaceIntervalCreditCell('O', "01:04", "01:12", 'D'),
                                                  new SurfaceIntervalCreditCell('O', "01:13", "01:34", 'C'),
                                                  new SurfaceIntervalCreditCell('O', "01:35", "02:23", 'B'),
                                                  new SurfaceIntervalCreditCell('O', "02:24", "05:24", 'A'),

                                                  // P
                                                  new SurfaceIntervalCreditCell('P', "00:00", "00:03", 'P'),
                                                  new SurfaceIntervalCreditCell('P', "00:04", "00:07", 'O'),
                                                  new SurfaceIntervalCreditCell('P', "00:08", "00:12", 'N'),
                                                  new SurfaceIntervalCreditCell('P', "00:13", "00:16", 'M'),
                                                  new SurfaceIntervalCreditCell('P', "00:17", "00:21", 'L'),
                                                  new SurfaceIntervalCreditCell('P', "00:22", "00:27", 'K'),
                                                  new SurfaceIntervalCreditCell('P', "00:28", "00:32", 'J'),
                                                  new SurfaceIntervalCreditCell('P', "00:33", "00:38", 'I'),
                                                  new SurfaceIntervalCreditCell('P', "00:39", "00:45", 'H'),
                                                  new SurfaceIntervalCreditCell('P', "00:46", "00:51", 'G'),
                                                  new SurfaceIntervalCreditCell('P', "00:52", "00:59", 'F'),
                                                  new SurfaceIntervalCreditCell('P', "01:00", "01:07", 'E'),
                                                  new SurfaceIntervalCreditCell('P', "01:08", "01:16", 'D'),
                                                  new SurfaceIntervalCreditCell('P', "01:17", "01:38", 'C'),
                                                  new SurfaceIntervalCreditCell('P', "01:39", "02:27", 'B'),
                                                  new SurfaceIntervalCreditCell('P', "02:28", "05:28", 'A'),

                                                  // Q
                                                  new SurfaceIntervalCreditCell('Q', "00:00", "00:03", 'Q'),
                                                  new SurfaceIntervalCreditCell('Q', "00:04", "00:07", 'P'),
                                                  new SurfaceIntervalCreditCell('Q', "00:08", "00:11", 'O'),
                                                  new SurfaceIntervalCreditCell('Q', "00:12", "00:16", 'N'),
                                                  new SurfaceIntervalCreditCell('Q', "00:17", "00:20", 'M'),
                                                  new SurfaceIntervalCreditCell('Q', "00:21", "00:25", 'L'),
                                                  new SurfaceIntervalCreditCell('Q', "00:26", "00:30", 'K'),
                                                  new SurfaceIntervalCreditCell('Q', "00:31", "00:36", 'J'),
                                                  new SurfaceIntervalCreditCell('Q', "00:37", "00:42", 'I'),
                                                  new SurfaceIntervalCreditCell('Q', "00:43", "00:48", 'H'),
                                                  new SurfaceIntervalCreditCell('Q', "00:49", "00:55", 'G'),
                                                  new SurfaceIntervalCreditCell('Q', "00:56", "01:03", 'F'),
                                                  new SurfaceIntervalCreditCell('Q', "01:04", "01:11", 'E'),
                                                  new SurfaceIntervalCreditCell('Q', "01:12", "01:20", 'D'),
                                                  new SurfaceIntervalCreditCell('Q', "01:21", "01:42", 'C'),
                                                  new SurfaceIntervalCreditCell('Q', "01:43", "02:30", 'B'),
                                                  new SurfaceIntervalCreditCell('Q', "02:31", "05:31", 'A'),

                                                  // R
                                                  new SurfaceIntervalCreditCell('R', "00:00", "00:03", 'R'),
                                                  new SurfaceIntervalCreditCell('R', "00:04", "00:07", 'Q'),
                                                  new SurfaceIntervalCreditCell('R', "00:08", "00:11", 'P'),
                                                  new SurfaceIntervalCreditCell('R', "00:12", "00:15", 'O'),
                                                  new SurfaceIntervalCreditCell('R', "00:16", "00:19", 'N'),
                                                  new SurfaceIntervalCreditCell('R', "00:20", "00:24", 'M'),
                                                  new SurfaceIntervalCreditCell('R', "00:25", "00:29", 'L'),
                                                  new SurfaceIntervalCreditCell('R', "00:30", "00:34", 'K'),
                                                  new SurfaceIntervalCreditCell('R', "00:35", "00:40", 'J'),
                                                  new SurfaceIntervalCreditCell('R', "00:41", "00:46", 'I'),
                                                  new SurfaceIntervalCreditCell('R', "00:47", "00:52", 'H'),
                                                  new SurfaceIntervalCreditCell('R', "00:53", "00:59", 'G'),
                                                  new SurfaceIntervalCreditCell('R', "01:00", "01:07", 'F'),
                                                  new SurfaceIntervalCreditCell('R', "01:08", "01:15", 'E'),
                                                  new SurfaceIntervalCreditCell('R', "01:16", "01:24", 'D'),
                                                  new SurfaceIntervalCreditCell('R', "01:25", "01:46", 'C'),
                                                  new SurfaceIntervalCreditCell('R', "01:47", "02:34", 'B'),
                                                  new SurfaceIntervalCreditCell('R', "02:35", "05:35", 'A'),

                                                  // S
                                                  new SurfaceIntervalCreditCell('S', "00:00", "00:03", 'S'),
                                                  new SurfaceIntervalCreditCell('S', "00:04", "00:06", 'R'),
                                                  new SurfaceIntervalCreditCell('S', "00:07", "00:10", 'Q'),
                                                  new SurfaceIntervalCreditCell('S', "00:11", "00:14", 'P'),
                                                  new SurfaceIntervalCreditCell('S', "00:15", "00:18", 'O'),
                                                  new SurfaceIntervalCreditCell('S', "00:19", "00:23", 'N'),
                                                  new SurfaceIntervalCreditCell('S', "00:24", "00:27", 'M'),
                                                  new SurfaceIntervalCreditCell('S', "00:28", "00:32", 'L'),
                                                  new SurfaceIntervalCreditCell('S', "00:33", "00:38", 'K'),
                                                  new SurfaceIntervalCreditCell('S', "00:39", "00:43", 'J'),
                                                  new SurfaceIntervalCreditCell('S', "00:44", "00:49", 'I'),
                                                  new SurfaceIntervalCreditCell('S', "00:50", "00:56", 'H'),
                                                  new SurfaceIntervalCreditCell('S', "00:57", "01:03", 'G'),
                                                  new SurfaceIntervalCreditCell('S', "01:04", "01:10", 'F'),
                                                  new SurfaceIntervalCreditCell('S', "01:11", "01:18", 'E'),
                                                  new SurfaceIntervalCreditCell('S', "01:19", "01:27", 'D'),
                                                  new SurfaceIntervalCreditCell('S', "01:28", "01:49", 'C'),
                                                  new SurfaceIntervalCreditCell('S', "01:50", "02:38", 'B'),
                                                  new SurfaceIntervalCreditCell('S', "02:39", "05:39", 'A'),

                                                  // T
                                                  new SurfaceIntervalCreditCell('T', "00:00", "00:02", 'T'),
                                                  new SurfaceIntervalCreditCell('T', "00:03", "00:06", 'S'),
                                                  new SurfaceIntervalCreditCell('T', "00:07", "00:10", 'R'),
                                                  new SurfaceIntervalCreditCell('T', "00:11", "00:13", 'Q'),
                                                  new SurfaceIntervalCreditCell('T', "00:14", "00:17", 'P'),
                                                  new SurfaceIntervalCreditCell('T', "00:18", "00:22", 'O'),
                                                  new SurfaceIntervalCreditCell('T', "00:23", "00:26", 'N'),
                                                  new SurfaceIntervalCreditCell('T', "00:27", "00:31", 'M'),
                                                  new SurfaceIntervalCreditCell('T', "00:32", "00:36", 'L'),
                                                  new SurfaceIntervalCreditCell('T', "00:37", "00:41", 'K'),
                                                  new SurfaceIntervalCreditCell('T', "00:42", "00:47", 'J'),
                                                  new SurfaceIntervalCreditCell('T', "00:48", "00:53", 'I'),
                                                  new SurfaceIntervalCreditCell('T', "00:54", "00:59", 'H'),
                                                  new SurfaceIntervalCreditCell('T', "01:00", "01:06", 'G'),
                                                  new SurfaceIntervalCreditCell('T', "01:07", "01:13", 'F'),
                                                  new SurfaceIntervalCreditCell('T', "01:14", "01:22", 'E'),
                                                  new SurfaceIntervalCreditCell('T', "01:23", "01:31", 'D'),
                                                  new SurfaceIntervalCreditCell('T', "01:32", "01:53", 'C'),
                                                  new SurfaceIntervalCreditCell('T', "01:54", "02:41", 'B'),
                                                  new SurfaceIntervalCreditCell('T', "02:42", "05:42", 'A'),

                                                  // U
                                                  new SurfaceIntervalCreditCell('U', "00:00", "00:02", 'U'),
                                                  new SurfaceIntervalCreditCell('U', "00:03", "00:06", 'T'),
                                                  new SurfaceIntervalCreditCell('U', "00:07", "00:09", 'S'),
                                                  new SurfaceIntervalCreditCell('U', "00:10", "00:13", 'R'),
                                                  new SurfaceIntervalCreditCell('U', "00:14", "00:17", 'Q'),
                                                  new SurfaceIntervalCreditCell('U', "00:18", "00:21", 'P'),
                                                  new SurfaceIntervalCreditCell('U', "00:22", "00:25", 'O'),
                                                  new SurfaceIntervalCreditCell('U', "00:26", "00:29", 'N'),
                                                  new SurfaceIntervalCreditCell('U', "00:30", "00:34", 'M'),
                                                  new SurfaceIntervalCreditCell('U', "00:35", "00:39", 'L'),
                                                  new SurfaceIntervalCreditCell('U', "00:40", "00:44", 'K'),
                                                  new SurfaceIntervalCreditCell('U', "00:45", "00:50", 'J'),
                                                  new SurfaceIntervalCreditCell('U', "00:51", "00:56", 'I'),
                                                  new SurfaceIntervalCreditCell('U', "00:57", "01:02", 'H'),
                                                  new SurfaceIntervalCreditCell('U', "01:03", "01:09", 'G'),
                                                  new SurfaceIntervalCreditCell('U', "01:10", "01:17", 'F'),
                                                  new SurfaceIntervalCreditCell('U', "01:18", "01:25", 'E'),
                                                  new SurfaceIntervalCreditCell('U', "01:26", "01:34", 'D'),
                                                  new SurfaceIntervalCreditCell('U', "01:35", "01:56", 'C'),
                                                  new SurfaceIntervalCreditCell('U', "01:57", "02:44", 'B'),
                                                  new SurfaceIntervalCreditCell('U', "02:45", "05:45", 'A'),

                                                  // V
                                                  new SurfaceIntervalCreditCell('V', "00:00", "00:02", 'V'),
                                                  new SurfaceIntervalCreditCell('V', "00:03", "00:05", 'U'),
                                                  new SurfaceIntervalCreditCell('V', "00:06", "00:09", 'T'),
                                                  new SurfaceIntervalCreditCell('V', "00:10", "00:12", 'S'),
                                                  new SurfaceIntervalCreditCell('V', "00:13", "00:16", 'R'),
                                                  new SurfaceIntervalCreditCell('V', "00:17", "00:20", 'Q'),
                                                  new SurfaceIntervalCreditCell('V', "00:21", "00:24", 'P'),
                                                  new SurfaceIntervalCreditCell('V', "00:25", "00:28", 'O'),
                                                  new SurfaceIntervalCreditCell('V', "00:29", "00:33", 'N'),
                                                  new SurfaceIntervalCreditCell('V', "00:34", "00:37", 'M'),
                                                  new SurfaceIntervalCreditCell('V', "00:38", "00:42", 'L'),
                                                  new SurfaceIntervalCreditCell('V', "00:43", "00:47", 'K'),
                                                  new SurfaceIntervalCreditCell('V', "00:48", "00:54", 'J'),
                                                  new SurfaceIntervalCreditCell('V', "00:54", "00:59", 'I'),
                                                  new SurfaceIntervalCreditCell('V', "01:00", "01:05", 'H'),
                                                  new SurfaceIntervalCreditCell('V', "01:06", "01:12", 'G'),
                                                  new SurfaceIntervalCreditCell('V', "01:13", "01:20", 'F'),
                                                  new SurfaceIntervalCreditCell('V', "01:21", "01:28", 'E'),
                                                  new SurfaceIntervalCreditCell('V', "01:29", "01:37", 'D'),
                                                  new SurfaceIntervalCreditCell('V', "01:38", "01:59", 'C'),
                                                  new SurfaceIntervalCreditCell('V', "02:00", "02:47", 'B'),
                                                  new SurfaceIntervalCreditCell('V', "02:48", "05:48", 'A'),

                                                  // W
                                                  new SurfaceIntervalCreditCell('W', "00:00", "00:02", 'W'),
                                                  new SurfaceIntervalCreditCell('W', "00:03", "00:05", 'V'),
                                                  new SurfaceIntervalCreditCell('W', "00:06", "00:08", 'U'),
                                                  new SurfaceIntervalCreditCell('W', "00:09", "00:12", 'T'),
                                                  new SurfaceIntervalCreditCell('W', "00:13", "00:15", 'S'),
                                                  new SurfaceIntervalCreditCell('W', "00:16", "00:19", 'R'),
                                                  new SurfaceIntervalCreditCell('W', "00:20", "00:23", 'Q'),
                                                  new SurfaceIntervalCreditCell('W', "00:24", "00:27", 'P'),
                                                  new SurfaceIntervalCreditCell('W', "00:28", "00:31", 'O'),
                                                  new SurfaceIntervalCreditCell('W', "00:32", "00:36", 'N'),
                                                  new SurfaceIntervalCreditCell('W', "00:37", "00:40", 'M'),
                                                  new SurfaceIntervalCreditCell('W', "00:41", "00:45", 'L'),
                                                  new SurfaceIntervalCreditCell('W', "00:46", "00:50", 'K'),
                                                  new SurfaceIntervalCreditCell('W', "00:51", "00:56", 'J'),
                                                  new SurfaceIntervalCreditCell('W', "00:57", "01:02", 'I'),
                                                  new SurfaceIntervalCreditCell('W', "01:03", "01:08", 'H'),
                                                  new SurfaceIntervalCreditCell('W', "01:09", "01:15", 'G'),
                                                  new SurfaceIntervalCreditCell('W', "01:16", "01:23", 'F'),
                                                  new SurfaceIntervalCreditCell('W', "01:24", "01:31", 'E'),
                                                  new SurfaceIntervalCreditCell('W', "01:32", "01:40", 'D'),
                                                  new SurfaceIntervalCreditCell('W', "01:41", "02:02", 'C'),
                                                  new SurfaceIntervalCreditCell('W', "02:03", "02:50", 'B'),
                                                  new SurfaceIntervalCreditCell('W', "02:51", "05:51", 'A'),

                                                  // X
                                                  new SurfaceIntervalCreditCell('X', "00:00", "00:02", 'X'),
                                                  new SurfaceIntervalCreditCell('X', "00:03", "00:05", 'W'),
                                                  new SurfaceIntervalCreditCell('X', "00:06", "00:08", 'V'),
                                                  new SurfaceIntervalCreditCell('X', "00:09", "00:11", 'U'),
                                                  new SurfaceIntervalCreditCell('X', "00:12", "00:15", 'T'),
                                                  new SurfaceIntervalCreditCell('X', "00:16", "00:18", 'S'),
                                                  new SurfaceIntervalCreditCell('X', "00:19", "00:22", 'R'),
                                                  new SurfaceIntervalCreditCell('X', "00:23", "00:26", 'Q'),
                                                  new SurfaceIntervalCreditCell('X', "00:27", "00:30", 'P'),
                                                  new SurfaceIntervalCreditCell('X', "00:31", "00:34", 'O'),
                                                  new SurfaceIntervalCreditCell('X', "00:35", "00:39", 'N'),
                                                  new SurfaceIntervalCreditCell('X', "00:40", "00:43", 'M'),
                                                  new SurfaceIntervalCreditCell('X', "00:44", "00:48", 'L'),
                                                  new SurfaceIntervalCreditCell('X', "00:49", "00:53", 'K'),
                                                  new SurfaceIntervalCreditCell('X', "00:54", "00:59", 'J'),
                                                  new SurfaceIntervalCreditCell('X', "01:00", "01:05", 'I'),
                                                  new SurfaceIntervalCreditCell('X', "01:06", "01:11", 'H'),
                                                  new SurfaceIntervalCreditCell('X', "01:12", "01:18", 'G'),
                                                  new SurfaceIntervalCreditCell('X', "01:19", "01:26", 'F'),
                                                  new SurfaceIntervalCreditCell('X', "01:27", "01:34", 'E'),
                                                  new SurfaceIntervalCreditCell('X', "01:35", "01:43", 'D'),
                                                  new SurfaceIntervalCreditCell('X', "01:44", "02:05", 'C'),
                                                  new SurfaceIntervalCreditCell('X', "02:06", "02:53", 'B'),
                                                  new SurfaceIntervalCreditCell('X', "02:54", "05:54", 'A'),

                                                  // Y
                                                  new SurfaceIntervalCreditCell('Y', "00:00", "00:02", 'Y'),
                                                  new SurfaceIntervalCreditCell('Y', "00:03", "00:05", 'X'),
                                                  new SurfaceIntervalCreditCell('Y', "00:06", "00:08", 'W'),
                                                  new SurfaceIntervalCreditCell('Y', "00:09", "00:11", 'V'),
                                                  new SurfaceIntervalCreditCell('Y', "00:12", "00:14", 'U'),
                                                  new SurfaceIntervalCreditCell('Y', "00:15", "00:18", 'T'),
                                                  new SurfaceIntervalCreditCell('Y', "00:19", "00:21", 'S'),
                                                  new SurfaceIntervalCreditCell('Y', "00:22", "00:25", 'R'),
                                                  new SurfaceIntervalCreditCell('Y', "00:26", "00:29", 'Q'),
                                                  new SurfaceIntervalCreditCell('Y', "00:30", "00:33", 'P'),
                                                  new SurfaceIntervalCreditCell('Y', "00:34", "00:37", 'O'),
                                                  new SurfaceIntervalCreditCell('Y', "00:38", "00:41", 'N'),
                                                  new SurfaceIntervalCreditCell('Y', "00:42", "00:46", 'M'),
                                                  new SurfaceIntervalCreditCell('Y', "00:47", "00:51", 'L'),
                                                  new SurfaceIntervalCreditCell('Y', "00:52", "00:56", 'K'),
                                                  new SurfaceIntervalCreditCell('Y', "00:57", "01:02", 'J'),
                                                  new SurfaceIntervalCreditCell('Y', "01:03", "01:08", 'I'),
                                                  new SurfaceIntervalCreditCell('Y', "01:09", "01:14", 'H'),
                                                  new SurfaceIntervalCreditCell('Y', "01:15", "01:21", 'G'),
                                                  new SurfaceIntervalCreditCell('Y', "01:22", "01:29", 'F'),
                                                  new SurfaceIntervalCreditCell('Y', "01:30", "01:37", 'E'),
                                                  new SurfaceIntervalCreditCell('Y', "01:38", "01:46", 'D'),
                                                  new SurfaceIntervalCreditCell('Y', "01:47", "02:08", 'C'),
                                                  new SurfaceIntervalCreditCell('Y', "02:09", "02:56", 'B'),
                                                  new SurfaceIntervalCreditCell('Y', "02:57", "05:57", 'A'),

                                                  // Z
                                                  new SurfaceIntervalCreditCell('Z', "00:00", "00:02", 'Z'),
                                                  new SurfaceIntervalCreditCell('Z', "00:03", "00:05", 'Y'),
                                                  new SurfaceIntervalCreditCell('Z', "00:06", "00:08", 'X'),
                                                  new SurfaceIntervalCreditCell('Z', "00:09", "00:11", 'W'),
                                                  new SurfaceIntervalCreditCell('Z', "00:12", "00:14", 'V'),
                                                  new SurfaceIntervalCreditCell('Z', "00:15", "00:17", 'U'),
                                                  new SurfaceIntervalCreditCell('Z', "00:18", "00:20", 'T'),
                                                  new SurfaceIntervalCreditCell('Z', "00:21", "00:24", 'S'),
                                                  new SurfaceIntervalCreditCell('Z', "00:25", "00:28", 'R'),
                                                  new SurfaceIntervalCreditCell('Z', "00:29", "00:31", 'Q'),
                                                  new SurfaceIntervalCreditCell('Z', "00:32", "00:35", 'P'),
                                                  new SurfaceIntervalCreditCell('Z', "00:36", "00:40", 'O'),
                                                  new SurfaceIntervalCreditCell('Z', "00:41", "00:44", 'N'),
                                                  new SurfaceIntervalCreditCell('Z', "00:45", "00:49", 'M'),
                                                  new SurfaceIntervalCreditCell('Z', "00:50", "00:54", 'L'),
                                                  new SurfaceIntervalCreditCell('Z', "00:55", "00:59", 'K'),
                                                  new SurfaceIntervalCreditCell('Z', "01:00", "01:05", 'J'),
                                                  new SurfaceIntervalCreditCell('Z', "01:06", "01:11", 'I'),
                                                  new SurfaceIntervalCreditCell('Z', "01:12", "01:17", 'H'),
                                                  new SurfaceIntervalCreditCell('Z', "01:18", "01:24", 'G'),
                                                  new SurfaceIntervalCreditCell('Z', "01:25", "01:31", 'F'),
                                                  new SurfaceIntervalCreditCell('Z', "01:32", "01:40", 'E'),
                                                  new SurfaceIntervalCreditCell('Z', "01:41", "01:49", 'D'),
                                                  new SurfaceIntervalCreditCell('Z', "01:50", "02:11", 'C'),
                                                  new SurfaceIntervalCreditCell('Z', "02:12", "02:59", 'B'),
                                                  new SurfaceIntervalCreditCell('Z', "03:00", "06:00", 'A')
                                              };

            #endregion

            #region INITIALISE REPETITIVE DIVE CELLS

            _repetitiveDiveCells = new List<RepetitiveDiveCell>
                                       {
                                           // 10 m
                                           new RepetitiveDiveCell('Z', 10, 219, null),
                                           new RepetitiveDiveCell('Y', 10, 199, 20),
                                           new RepetitiveDiveCell('X', 10, 178, 41),
                                           new RepetitiveDiveCell('W', 10, 160, 59),
                                           new RepetitiveDiveCell('V', 10, 145, 74),
                                           new RepetitiveDiveCell('U', 10, 133, 86),
                                           new RepetitiveDiveCell('T', 10, 122, 97),
                                           new RepetitiveDiveCell('S', 10, 112, 107),
                                           new RepetitiveDiveCell('R', 10, 104, 115),
                                           new RepetitiveDiveCell('Q', 10, 95, 124),
                                           new RepetitiveDiveCell('P', 10, 88, 131),
                                           new RepetitiveDiveCell('O', 10, 82, 137),
                                           new RepetitiveDiveCell('N', 10, 75, 144),
                                           new RepetitiveDiveCell('M', 10, 70, 149),
                                           new RepetitiveDiveCell('L', 10, 64, 155),
                                           new RepetitiveDiveCell('K', 10, 59, 160),
                                           new RepetitiveDiveCell('J', 10, 54, 165),
                                           new RepetitiveDiveCell('I', 10, 50, 169),
                                           new RepetitiveDiveCell('H', 10, 45, 174),
                                           new RepetitiveDiveCell('G', 10, 41, 178),
                                           new RepetitiveDiveCell('F', 10, 37, 182),
                                           new RepetitiveDiveCell('E', 10, 34, 185),
                                           new RepetitiveDiveCell('D', 10, 30, 189),
                                           new RepetitiveDiveCell('C', 10, 26, 193),
                                           new RepetitiveDiveCell('B', 10, 20, 199),
                                           new RepetitiveDiveCell('A', 10, 10, 209),

                                           // 12 m
                                           new RepetitiveDiveCell('Z', 12, 147, null),
                                           new RepetitiveDiveCell('Y', 12, 134, 13),
                                           new RepetitiveDiveCell('X', 12, 125, 22),
                                           new RepetitiveDiveCell('W', 12, 116, 31),
                                           new RepetitiveDiveCell('V', 12, 108, 39),
                                           new RepetitiveDiveCell('U', 12, 101, 46),
                                           new RepetitiveDiveCell('T', 12, 94, 53),
                                           new RepetitiveDiveCell('S', 12, 88, 59),
                                           new RepetitiveDiveCell('R', 12, 82, 65),
                                           new RepetitiveDiveCell('Q', 12, 76, 71),
                                           new RepetitiveDiveCell('P', 12, 71, 76),
                                           new RepetitiveDiveCell('O', 12, 66, 81),
                                           new RepetitiveDiveCell('N', 12, 62, 85),
                                           new RepetitiveDiveCell('M', 12, 57, 90),
                                           new RepetitiveDiveCell('L', 12, 53, 94),
                                           new RepetitiveDiveCell('K', 12, 49, 98),
                                           new RepetitiveDiveCell('J', 12, 45, 102),
                                           new RepetitiveDiveCell('I', 12, 42, 105),
                                           new RepetitiveDiveCell('H', 12, 38, 109),
                                           new RepetitiveDiveCell('G', 12, 35, 112),
                                           new RepetitiveDiveCell('F', 12, 32, 115),
                                           new RepetitiveDiveCell('E', 12, 29, 118),
                                           new RepetitiveDiveCell('D', 12, 26, 121),
                                           new RepetitiveDiveCell('C', 12, 23, 124),
                                           new RepetitiveDiveCell('B', 12, 17, 130),
                                           new RepetitiveDiveCell('A', 12, 9, 138),

                                           // 14 m
                                           new RepetitiveDiveCell('Y', 14, 98, null),
                                           new RepetitiveDiveCell('X', 14, 92, 6),
                                           new RepetitiveDiveCell('W', 14, 87, 11),
                                           new RepetitiveDiveCell('V', 14, 82, 16),
                                           new RepetitiveDiveCell('U', 14, 77, 21),
                                           new RepetitiveDiveCell('T', 14, 73, 25),
                                           new RepetitiveDiveCell('S', 14, 68, 30),
                                           new RepetitiveDiveCell('R', 14, 64, 34),
                                           new RepetitiveDiveCell('Q', 14, 61, 37),
                                           new RepetitiveDiveCell('P', 14, 57, 41),
                                           new RepetitiveDiveCell('O', 14, 53, 45),
                                           new RepetitiveDiveCell('N', 14, 50, 48),
                                           new RepetitiveDiveCell('M', 14, 47, 51),
                                           new RepetitiveDiveCell('L', 14, 43, 55),
                                           new RepetitiveDiveCell('K', 14, 40, 58),
                                           new RepetitiveDiveCell('J', 14, 37, 61),
                                           new RepetitiveDiveCell('I', 14, 35, 63),
                                           new RepetitiveDiveCell('H', 14, 32, 66),
                                           new RepetitiveDiveCell('G', 14, 29, 69),
                                           new RepetitiveDiveCell('F', 14, 27, 71),
                                           new RepetitiveDiveCell('E', 14, 24, 74),
                                           new RepetitiveDiveCell('D', 14, 22, 76),
                                           new RepetitiveDiveCell('C', 14, 19, 79),
                                           new RepetitiveDiveCell('B', 14, 15, 83),
                                           new RepetitiveDiveCell('A', 14, 8, 90),

                                           // 16 m
                                           new RepetitiveDiveCell('X', 16, 72, null),
                                           new RepetitiveDiveCell('W', 16, 70, 2),
                                           new RepetitiveDiveCell('V', 16, 67, 5),
                                           new RepetitiveDiveCell('U', 16, 63, 9),
                                           new RepetitiveDiveCell('T', 16, 60, 12),
                                           new RepetitiveDiveCell('S', 16, 56, 16),
                                           new RepetitiveDiveCell('R', 16, 53, 19),
                                           new RepetitiveDiveCell('Q', 16, 50, 22),
                                           new RepetitiveDiveCell('P', 16, 48, 24),
                                           new RepetitiveDiveCell('O', 16, 45, 27),
                                           new RepetitiveDiveCell('N', 16, 42, 30),
                                           new RepetitiveDiveCell('M', 16, 39, 33),
                                           new RepetitiveDiveCell('L', 16, 37, 35),
                                           new RepetitiveDiveCell('K', 16, 34, 38),
                                           new RepetitiveDiveCell('J', 16, 32, 40),
                                           new RepetitiveDiveCell('I', 16, 29, 43),
                                           new RepetitiveDiveCell('H', 16, 27, 45),
                                           new RepetitiveDiveCell('G', 16, 25, 47),
                                           new RepetitiveDiveCell('F', 16, 23, 49),
                                           new RepetitiveDiveCell('E', 16, 21, 51),
                                           new RepetitiveDiveCell('D', 16, 19, 53),
                                           new RepetitiveDiveCell('C', 16, 17, 55),
                                           new RepetitiveDiveCell('B', 16, 13, 59),
                                           new RepetitiveDiveCell('A', 16, 7, 65),

                                           // 18 m
                                           new RepetitiveDiveCell('W', 18, 56, null),
                                           new RepetitiveDiveCell('V', 18, 55, null),
                                           new RepetitiveDiveCell('U', 18, 53, 3),
                                           new RepetitiveDiveCell('T', 18, 51, 5),
                                           new RepetitiveDiveCell('S', 18, 48, 8),
                                           new RepetitiveDiveCell('R', 18, 46, 10),
                                           new RepetitiveDiveCell('Q', 18, 43, 13),
                                           new RepetitiveDiveCell('P', 18, 41, 15),
                                           new RepetitiveDiveCell('O', 18, 39, 17),
                                           new RepetitiveDiveCell('N', 18, 36, 20),
                                           new RepetitiveDiveCell('M', 18, 34, 22),
                                           new RepetitiveDiveCell('L', 18, 32, 24),
                                           new RepetitiveDiveCell('K', 18, 30, 26),
                                           new RepetitiveDiveCell('J', 18, 28, 28),
                                           new RepetitiveDiveCell('I', 18, 26, 30),
                                           new RepetitiveDiveCell('H', 18, 24, 32),
                                           new RepetitiveDiveCell('G', 18, 22, 34),
                                           new RepetitiveDiveCell('F', 18, 20, 36),
                                           new RepetitiveDiveCell('E', 18, 18, 38),
                                           new RepetitiveDiveCell('D', 18, 16, 40),
                                           new RepetitiveDiveCell('C', 18, 15, 41),
                                           new RepetitiveDiveCell('B', 18, 11, 45),
                                           new RepetitiveDiveCell('A', 18, 6, 50),

                                           // 20 m
                                           new RepetitiveDiveCell('U', 20, 45, null),
                                           new RepetitiveDiveCell('T', 20, 44, null),
                                           new RepetitiveDiveCell('S', 20, 42, 3),
                                           new RepetitiveDiveCell('R', 20, 40, 5),
                                           new RepetitiveDiveCell('Q', 20, 38, 7),
                                           new RepetitiveDiveCell('P', 20, 36, 9),
                                           new RepetitiveDiveCell('O', 20, 34, 11),
                                           new RepetitiveDiveCell('N', 20, 32, 13),
                                           new RepetitiveDiveCell('M', 20, 30, 15),
                                           new RepetitiveDiveCell('L', 20, 28, 17),
                                           new RepetitiveDiveCell('K', 20, 26, 19),
                                           new RepetitiveDiveCell('J', 20, 25, 20),
                                           new RepetitiveDiveCell('I', 20, 23, 22),
                                           new RepetitiveDiveCell('H', 20, 21, 24),
                                           new RepetitiveDiveCell('G', 20, 20, 25),
                                           new RepetitiveDiveCell('F', 20, 18, 27),
                                           new RepetitiveDiveCell('E', 20, 16, 29),
                                           new RepetitiveDiveCell('D', 20, 15, 30),
                                           new RepetitiveDiveCell('C', 20, 13, 32),
                                           new RepetitiveDiveCell('B', 20, 10, 35),
                                           new RepetitiveDiveCell('A', 20, 6, 39),

                                           // 22 m
                                           new RepetitiveDiveCell('S', 22, 37, null),
                                           new RepetitiveDiveCell('R', 22, 36, null),
                                           new RepetitiveDiveCell('Q', 22, 34, 3),
                                           new RepetitiveDiveCell('P', 22, 32, 5),
                                           new RepetitiveDiveCell('O', 22, 30, 7),
                                           new RepetitiveDiveCell('N', 22, 29, 8),
                                           new RepetitiveDiveCell('M', 22, 27, 10),
                                           new RepetitiveDiveCell('L', 22, 25, 12),
                                           new RepetitiveDiveCell('K', 22, 24, 13),
                                           new RepetitiveDiveCell('J', 22, 22, 15),
                                           new RepetitiveDiveCell('I', 22, 21, 16),
                                           new RepetitiveDiveCell('H', 22, 19, 18),
                                           new RepetitiveDiveCell('G', 22, 18, 19),
                                           new RepetitiveDiveCell('F', 22, 16, 21),
                                           new RepetitiveDiveCell('E', 22, 15, 22),
                                           new RepetitiveDiveCell('D', 22, 13, 24),
                                           new RepetitiveDiveCell('C', 22, 12, 25),
                                           new RepetitiveDiveCell('B', 22, 9, 28),
                                           new RepetitiveDiveCell('A', 22, 5, 32),

                                           // 25 m
                                           new RepetitiveDiveCell('Q', 25, 29, null),
                                           new RepetitiveDiveCell('P', 25, 28, null),
                                           new RepetitiveDiveCell('O', 25, 26, 3),
                                           new RepetitiveDiveCell('N', 25, 25, 4),
                                           new RepetitiveDiveCell('M', 25, 23, 6),
                                           new RepetitiveDiveCell('L', 25, 22, 7),
                                           new RepetitiveDiveCell('K', 25, 21, 8),
                                           new RepetitiveDiveCell('J', 25, 19, 10),
                                           new RepetitiveDiveCell('I', 25, 18, 11),
                                           new RepetitiveDiveCell('H', 25, 17, 12),
                                           new RepetitiveDiveCell('G', 25, 15, 14),
                                           new RepetitiveDiveCell('F', 25, 14, 15),
                                           new RepetitiveDiveCell('E', 25, 13, 16),
                                           new RepetitiveDiveCell('D', 25, 11, 18),
                                           new RepetitiveDiveCell('C', 25, 10, 19),
                                           new RepetitiveDiveCell('B', 25, 8, 21),
                                           new RepetitiveDiveCell('A', 25, 4, 25),

                                           // 30 m
                                           new RepetitiveDiveCell('N', 30, 20, null),
                                           new RepetitiveDiveCell('M', 30, 19, null),
                                           new RepetitiveDiveCell('L', 30, 17, 3),
                                           new RepetitiveDiveCell('K', 30, 16, 4),
                                           new RepetitiveDiveCell('J', 30, 15, 5),
                                           new RepetitiveDiveCell('I', 30, 14, 6),
                                           new RepetitiveDiveCell('H', 30, 13, 7),
                                           new RepetitiveDiveCell('G', 30, 12, 8),
                                           new RepetitiveDiveCell('F', 30, 11, 9),
                                           new RepetitiveDiveCell('E', 30, 10, 10),
                                           new RepetitiveDiveCell('D', 30, 9, 11),
                                           new RepetitiveDiveCell('C', 30, 8, 12),
                                           new RepetitiveDiveCell('B', 30, 6, 14),
                                           new RepetitiveDiveCell('A', 30, 3, 17),

                                           // 35 m
                                           new RepetitiveDiveCell('K', 35, 14, null),
                                           new RepetitiveDiveCell('J', 35, 13, null),
                                           new RepetitiveDiveCell('I', 35, 12, null),
                                           new RepetitiveDiveCell('H', 35, 11, 3),
                                           new RepetitiveDiveCell('G', 35, 10, 4),
                                           new RepetitiveDiveCell('F', 35, 9, 5),
                                           new RepetitiveDiveCell('E', 35, 9, 5),
                                           new RepetitiveDiveCell('D', 35, 8, 6),
                                           new RepetitiveDiveCell('C', 35, 7, 7),
                                           new RepetitiveDiveCell('B', 35, 5, 9),
                                           new RepetitiveDiveCell('A', 35, 3, 11),

                                           // 40 m
                                           new RepetitiveDiveCell('G', 40, 9, null),
                                           new RepetitiveDiveCell('F', 40, 8, null),
                                           new RepetitiveDiveCell('E', 40, 7, null),
                                           new RepetitiveDiveCell('D', 40, 7, null),
                                           new RepetitiveDiveCell('C', 40, 6, 3), // this is NULL on the tables but 3 in the eRDPMl
                                           new RepetitiveDiveCell('B', 40, 5, 4),
                                           new RepetitiveDiveCell('A', 40, 2, 7)
                                       };

            #endregion
        }

        /// <summary>Gets the pressure group.</summary>
        /// <param name="depth">The depth.</param>
        /// <param name="duration">The duration.</param>
        /// <returns>The pressure group or NULL or depth and/or duration exceeds table limits</returns>
        internal char? GetPressureGroup(int depth, int duration)
        {
            TimeCell timeCell = GetTimeCell(depth, duration);
            return timeCell != null ? (char?)timeCell.EndingPressureGroup : null;
        }

        /// <summary>Gets the pressure group after a surface interval</summary>
        /// <param name="startPressureGroup">The start pressure group.</param>
        /// <param name="duration">The duration.</param>
        /// <returns>The ending pressure group</returns>
        internal char GetPressureGroupAfterSurfaceInterval(char startPressureGroup, TimeSpan duration)
        {
            SurfaceIntervalCreditCell cell = _surfaceIntervalCreditCells.FirstOrDefault(x => x.StartPressureGroup == startPressureGroup && x.From <= duration && x.To >= duration);
            return cell != null ? cell.EndingPressureGroup : MinPressureGroup;
        }

        /// <summary>Gets the maximum timecell for depth.</summary>
        /// <param name="depth">The depth.</param>
        /// <returns>The timecell or NULL if depth exceeds table limits</returns>
        internal TimeCell GetMaxForDepth(int depth)
        {
            int? tableDepth = GetTableDepth(depth);
            return tableDepth != null ? _timeCells.LastOrDefault(x => x.Depth == tableDepth) : null;
        }

        /// <summary>Snaps a depth to a depth on the table</summary>
        /// <param name="depth">The depth.</param>
        /// <returns>A table depth. Null if over Max table depth</returns>
        internal int? GetTableDepth(int depth)
        {
            TimeCell columnToUse = _timeCells.FirstOrDefault(x => x.Depth >= depth);
            return columnToUse != null ? (int?)columnToUse.Depth : null;
        }

        /// <summary>Gets the exact or nearest time cell UP</summary>
        /// <param name="depth">The depth.</param>
        /// <param name="duration">The duration.</param>
        /// <returns>The timecell or NULL or exceeds table limits</returns>
        internal TimeCell GetTimeCell(int depth, int duration)
        {
            return _timeCells.FirstOrDefault(x => x.Depth >= depth && x.Duration >= duration);
        }

        /// <summary>Gets the no decompression limit for depth.</summary>
        /// <param name="depth">The depth.</param>
        /// <returns>The NDL or NULL or exceeds table limits</returns>
        internal int? GetNoDecompressionLimitForDepth(int depth)
        {
            int? tableDepth = GetTableDepth(depth);
            return tableDepth != null ? _timeCells.Last(x => x.Depth == tableDepth).Duration : null;
        }

        /// <summary>Gets the repetitive dive cell.</summary>
        /// <param name="pressureGroup">The pressure group.</param>
        /// <param name="depth">The depth.</param>
        /// <returns>The repetive Dive Cell or NULL if outside table limits</returns>
        internal RepetitiveDiveCell GetRepetitiveDiveCell(char pressureGroup, int depth)
        {
            return _repetitiveDiveCells.FirstOrDefault(x => x.PressureGroup == pressureGroup && x.Depth >= depth);
        }

        /// <summary>Calculates the dive plan.</summary>
        /// <param name="dives">The dives.</param>
        /// <returns>The dive plan - all populated</returns>
        public List<Dive> CalculateDivePlan(List<Dive> dives)
        {
            if (dives.Any() == false)
            {
                return null;
            }

            if (dives[0].PressureGroupBeforeDive == null)
            {
                dives[0].PressureGroupBeforeDive = MinPressureGroup;
            }

            List<Dive> divePlan = new List<Dive>();

            for (int i = 0; i < dives.Count; i++)
            {
                Dive dive = dives[i];

                dive.TotalBottomTime = dive.PlannedBottomTime + dive.ResidualNitrogenTimeBroughtForward;

                if (dive.AdjustedNoDecompressionLimitBroughtForward == 0 || dive.PlannedBottomTime <= dive.AdjustedNoDecompressionLimitBroughtForward)
                {
                    // it's within the no decompression limit set by the previous dive
                    TimeCell timeCell = GetTimeCell(dive.PlannedDepth, dive.TotalBottomTime.Value);

                    if (timeCell != null)
                    {
                        dive.PressureGroupAfterDive = timeCell.EndingPressureGroup;
                        dive.SafetyStop = timeCell.SafetyStopRrequired;

                        if (i + 1 < dives.Count)
                        {
                            // there's a dive following this one

                            SurfaceInterval si = GetMinimumSurfaceInterval(dive.PressureGroupAfterDive.Value, dives[i + 1].PlannedDepth, dives[i + 1].PlannedBottomTime, dive.SurfaceInterval);

                            if (si != null)
                            {
                                // we can do the following dive
                                dive.ResidualNitrogenTimeToCarryForward = si.ResidualNitrogenTime;
                                dive.AdjustedNoDecompressionLimitToCarryForward = si.AdjustedNoDecompressionLimit;
                                dive.SurfaceInterval = si.MinimumSurfaceInterval;
                                dive.PressureGroupAfterSurfaceInterval = si.PressureGroupAfterSurfaceInterval;

                                dives[i + 1].ResidualNitrogenTimeBroughtForward = si.ResidualNitrogenTime;
                                dives[i + 1].AdjustedNoDecompressionLimitBroughtForward = si.AdjustedNoDecompressionLimit;
                                dives[i + 1].PressureGroupBeforeDive = si.PressureGroupAfterSurfaceInterval;
                            }
                        }
                        //else
                        //{
                        //    // this is the last dive
                        //}

                        // all good - add it to the plan
                        divePlan.Add(dive);
                    }
                    //else
                    //{
                    //    // we can't do this dive
                    //}
                }
                //else
                //{
                //    // we can't do the dive
                //    // the ANDL brought forward from the previous dive is preventing us from doing it
                //}
            }

            return divePlan;
        }

        /// <summary>Gets the minimum surface interval.</summary>
        /// <param name="pressureGroupBeforeSurfaceInterval">The pressure group before surface interval.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="requestedMinimumSurfaceInterval">The requested minimum surface interval.</param>
        /// <returns>The surface interval information</returns>
        internal SurfaceInterval GetMinimumSurfaceInterval(char pressureGroupBeforeSurfaceInterval, int depth, int duration, TimeSpan? requestedMinimumSurfaceInterval = null)
        {
            TimeSpan minimumSurfaceInterval = AdjustMinimumSurfaceIntervalForRuleWxyz(pressureGroupBeforeSurfaceInterval);

            char originalPressureGroupBeforeSurfaceInterval = pressureGroupBeforeSurfaceInterval;

            if (requestedMinimumSurfaceInterval != null && requestedMinimumSurfaceInterval.Value > minimumSurfaceInterval)
            {
                minimumSurfaceInterval = requestedMinimumSurfaceInterval.Value;
            }

            if (minimumSurfaceInterval.TotalMinutes > 0)
            {
                // apply a surface intervale before we start
                SurfaceIntervalCreditCell surfaceIntervalCreditCell = _surfaceIntervalCreditCells.FirstOrDefault(x => x.StartPressureGroup == pressureGroupBeforeSurfaceInterval && x.To >= minimumSurfaceInterval);
                if (surfaceIntervalCreditCell != null)
                {
                    pressureGroupBeforeSurfaceInterval = surfaceIntervalCreditCell.EndingPressureGroup;
                }
                else
                {
                    // surface interval was longer than what's covered by the table - drop down to an 'A'
                    pressureGroupBeforeSurfaceInterval = MinPressureGroup;
                }
            }

            RepetitiveDiveCell repetitiveDiveCell = _repetitiveDiveCells.FirstOrDefault(x => x.Depth >= depth && x.PressureGroup <= pressureGroupBeforeSurfaceInterval && x.AdjustedNoDecompressionLimit >= duration);

            if (repetitiveDiveCell != null && repetitiveDiveCell.AdjustedNoDecompressionLimit != null)
            {
                SurfaceIntervalCreditCell surfaceIntervalCreditCell = _surfaceIntervalCreditCells.FirstOrDefault(x => x.StartPressureGroup == originalPressureGroupBeforeSurfaceInterval && x.EndingPressureGroup <= repetitiveDiveCell.PressureGroup);

                return new SurfaceInterval(originalPressureGroupBeforeSurfaceInterval, surfaceIntervalCreditCell.From, surfaceIntervalCreditCell.To, surfaceIntervalCreditCell.EndingPressureGroup, repetitiveDiveCell.ResidualNitrogenTime, repetitiveDiveCell.AdjustedNoDecompressionLimit.Value);
            }

            // can't do the dive
            return null;
        }

        internal static TimeSpan AdjustMinimumSurfaceIntervalForRuleWxyz(char pressureGroupBeforeSurfaceInterval)
        {
            TimeSpan minimumSurfaceInterval;
            switch (pressureGroupBeforeSurfaceInterval)
            {
                case 'W':
                case 'X':
                    {
                        // 1 hour
                        minimumSurfaceInterval = new TimeSpan(1, 0, 0);
                        break;
                    }
                case 'Y':
                case 'Z':
                    {
                        // 3 hours
                        minimumSurfaceInterval = new TimeSpan(3, 0, 0);
                        break;
                    }
                default:
                    {
                        // no minimum surface interval
                        minimumSurfaceInterval = new TimeSpan(0, 0, 0);
                        break;
                    }
            }
            return minimumSurfaceInterval;
        }
    }

    internal class TimeCell
    {
        public TimeCell(int depth, int? duration, char endingPressureGroup, bool advanceToNextDuration = false, bool safetyStopRequired = false, bool noDecompressionLimit = false)
        {
            Depth = depth;
            Duration = duration;
            EndingPressureGroup = endingPressureGroup;
            AdvanceToNextDuration = advanceToNextDuration;
            SafetyStopRrequired = safetyStopRequired;
            NoDecompressionLimit = noDecompressionLimit;
        }

        public int Depth { get; private set; }
        public int? Duration { get; private set; }
        public bool SafetyStopRrequired { get; private set; }
        public bool NoDecompressionLimit { get; private set; }
        public char EndingPressureGroup { get; private set; }
        public bool AdvanceToNextDuration { get; private set; }
    }

    internal class SurfaceIntervalCreditCell
    {
        public SurfaceIntervalCreditCell(char startPressureGroup, string startTimeSpan, string endTimeSpan, char endingPressureGroup)
        {
            StartPressureGroup = startPressureGroup;
            From = TimeSpan.Parse(startTimeSpan);
            To = TimeSpan.Parse(endTimeSpan);
            EndingPressureGroup = endingPressureGroup;
        }

        public char StartPressureGroup { get; private set; }
        public TimeSpan From { get; private set; }
        public TimeSpan To { get; private set; }
        public char EndingPressureGroup { get; private set; }
    }

    public class SurfaceInterval
    {
        public SurfaceInterval(char pressureGroupBeforeSurfaceInterval, TimeSpan minimumSurfaceInterval, TimeSpan maximumSurfaceInterval, char pressureGroupAfterSurfaceInterval, int residualNitrogenTime, int adjustedNoDecompressionLimit)
        {
            PressureGroupBeforeSurfaceInterval = pressureGroupBeforeSurfaceInterval;
            MinimumSurfaceInterval = minimumSurfaceInterval;
            MaximumSurfaceInterval = maximumSurfaceInterval;
            PressureGroupAfterSurfaceInterval = pressureGroupAfterSurfaceInterval;
            ResidualNitrogenTime = residualNitrogenTime;
            AdjustedNoDecompressionLimit = adjustedNoDecompressionLimit;
        }

        public char PressureGroupBeforeSurfaceInterval { get; private set; }
        public TimeSpan MinimumSurfaceInterval { get; private set; }
        public TimeSpan MaximumSurfaceInterval { get; private set; }
        public char PressureGroupAfterSurfaceInterval { get; private set; }
        public int ResidualNitrogenTime { get; private set; }
        public int AdjustedNoDecompressionLimit { get; private set; }
    }

    public class RepetitiveDiveCell
    {
        public RepetitiveDiveCell(char pressureGroup, int depth, int residualNitrogenTime, int? adjustedNoDecompressionLimit)
        {
            PressureGroup = pressureGroup;
            Depth = depth;
            ResidualNitrogenTime = residualNitrogenTime;
            AdjustedNoDecompressionLimit = adjustedNoDecompressionLimit;
        }

        public char PressureGroup { get; private set; }
        public int Depth { get; private set; }
        public int ResidualNitrogenTime { get; private set; }
        public int? AdjustedNoDecompressionLimit { get; private set; }
    }

    public class Dive
    {
        public Dive(int plannedDepth, int plannedBottomTime, TimeSpan? minimumSurfaceInterval = null)
        {
            PlannedDepth = plannedDepth;
            PlannedBottomTime = plannedBottomTime;
            SurfaceInterval = minimumSurfaceInterval;
        }

        public int ResidualNitrogenTimeBroughtForward { get; internal set; }
        public int AdjustedNoDecompressionLimitBroughtForward { get; internal set; }
        public int ResidualNitrogenTimeToCarryForward { get; internal set; }
        public int AdjustedNoDecompressionLimitToCarryForward { get; internal set; }
        public char? PressureGroupBeforeDive { get; internal set; }
        public char? PressureGroupAfterDive { get; internal set; }
        public int PlannedDepth { get; private set; }
        public int PlannedBottomTime { get; private set; }
        public bool? SafetyStop { get; internal set; }
        public TimeSpan? SurfaceInterval { get; internal set; }
        public char? PressureGroupAfterSurfaceInterval { get; internal set; }
        public int? TotalBottomTime { get; internal set; }
    }
}