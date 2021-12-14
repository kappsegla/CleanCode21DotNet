namespace Shop
{
    public class Item
    {

        public string name;

        public int sellIn;

        public int quality;

        public Item(string name, int sellIn, int quality)
        {
            this.name = name;
            this.sellIn = sellIn;
            this.quality = quality;
        }

        public override string ToString()
        {
            return this.name + ", " + this.sellIn + ", " + this.quality;
        }
    }
}