namespace shared.Model;

public class PN : Ordination {
	public double antalEnheder { get; set; }
    public List<Dato> dates { get; set; } = new List<Dato>();

    public PN (DateTime startDen, DateTime slutDen, double antalEnheder, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen) {
		this.antalEnheder = antalEnheder;
	}

    public PN() : base(null!, new DateTime(), new DateTime()) {
    }

    /// <summary>
    /// Registrerer at der er givet en dosis på dagen givesDen
    /// Returnerer true hvis givesDen er inden for ordinationens gyldighedsperiode og datoen huskes
    /// Returner false ellers og datoen givesDen ignoreres
    /// </summary>
    public bool givDosis(Dato givesDen)
    {
        
                return false;
            }

     





    public override double doegnDosis() {

       /* if (dates.Count < 2)
        {
            // Hvis der er færre end to datoer, kan døgndosis ikke beregnes korrekt.
            return -1; // Du kan vælge at returnere en særlig værdi for fejl, f.eks. -1.
        }*/

        // Antal gange ordinationen er anvendt
        int antalGange = dates.Count();

        // Antal dage mellem første og sidste givning
        int antalDage = (int)(slutDen - startDen).TotalDays + 1;


        // Beregning af døgndosis
        return (antalGange * antalEnheder) / antalDage;
    }



    public override double samletDosis() {
        return dates.Count() * antalEnheder;
    }

    public int getAntalGangeGivet() {
        return dates.Count();
    }

	public override String getType() {
		return "PN";
	}
}
