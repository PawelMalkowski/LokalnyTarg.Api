namespace LokalnyTarg.Data.Sql.DAO
{
    public class FavoriteSuppillier
    {
        public uint FavoriteSuppillierId { get; set; }
        public uint SuppilierId { get; set; }
        public uint UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Supplier Suppllier { get; set; } // re sharper podpowadaczka
    }
}