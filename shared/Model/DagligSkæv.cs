namespace shared.Model;

public class DagligSkæv : Ordination {
    public List<Dosis> doser { get; set; } = new List<Dosis>();

    public DagligSkæv(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen) {
	}

    public DagligSkæv(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel, Dosis[] doser) : base(laegemiddel, startDen, slutDen) {
        this.doser = doser.ToList();
    }    

    public DagligSkæv() : base(null!, new DateTime(), new DateTime()) {
    }

	public void opretDosis(DateTime tid, double antal) {
        doser.Add(new Dosis(tid, antal));
    }

	public override double samletDosis() {
		return base.antalDage() * doegnDosis();
	}

	
	/// <summary>
	/// returnerer total antal doser i ordinationen
	/// loop igennem doser og læg antal sammen
	public override double doegnDosis() {
		if (doser.Count == 0)
			return 0;

		double total = 0;
		foreach (Dosis dosis in doser) { 
			total += dosis.antal;
		}
		return total;
	}


	public override String getType() {
		return "DagligSkæv";
	}
}
