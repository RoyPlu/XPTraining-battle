using System;

namespace Battle
{
    public interface IHeadquarters
    {
        Guid ReportEnlistment(string soldierName);
    }
}