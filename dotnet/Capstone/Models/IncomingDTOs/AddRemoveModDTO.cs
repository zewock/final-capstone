namespace Capstone.Models.IncomingDTOs
{
    public class AddRemoveMod
    {
        public int formID { get; set; }

        public AddRemoveMod()
        {

        }

        public AddRemoveMod(int formID)
        {
            this.formID = formID;
        }
    }
}
