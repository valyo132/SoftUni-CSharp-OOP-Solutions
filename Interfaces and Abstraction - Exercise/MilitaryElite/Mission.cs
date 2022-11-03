namespace MilitaryElite
{
    using System;

    public class Mission
    {
        public Mission(string codeName,string state)
        {
            this.State = state;
            this.CodeName = codeName;
        }

        public string CodeName { get; set; }

        public string State { get; set; }

        public void CompleteMission()
        {
            this.State = "Finished";
        }

        public override string ToString()
           => $"Code Name: {this.CodeName} State: {this.State}";
    }
}
