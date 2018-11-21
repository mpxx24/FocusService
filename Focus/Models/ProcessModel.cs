namespace Focus.Models {
    public class ProcessModel {
        private int id { get; set; }

        private string name { get; set; }

        public int Id {
            get => this.id;
            set => this.id = value;
        }

        public string Name {
            get => this.name;
            set => this.name = value;
        }
    }
}