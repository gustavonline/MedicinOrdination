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
        if (givesDen.dato >= startDen && givesDen.dato <= slutDen)
        {
            dates.Add(givesDen);
            return true;
        }
        return false;
    }


    public override double doegnDosis() {
        HashSet<Dato> unikkeDatoer = new HashSet<Dato>(dates);

        // Antal unikke dage, hvor ordinationen er anvendt
        int antalUnikkeDage = unikkeDatoer.Count;

        // Antal dage mellem første og sidste givning
        int antalDage = (int)(slutDen - startDen).TotalDays + 1;

        // Sikre, at der er nok dage til at foretage en beregning
        if (antalDage < 1 || antalUnikkeDage < 1)
            return -1; // Returnerer -1 eller en anden fejlværdi

        // Beregning af døgndosis
        return (antalUnikkeDage * antalEnheder) / antalDage;
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
