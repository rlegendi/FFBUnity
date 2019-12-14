﻿namespace Fumbbl.Ffb.Dto.Reports
{
    public class InducementsBought : Report
    {
        public string reportId;
        public string teamId;
        public int nrOfInducements;
        public int nrOfStars;
        public int nrOfMercenaries;
        public int gold;

        public InducementsBought() : base("inducementsBought") { }
    }
}
