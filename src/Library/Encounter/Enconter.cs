namespace Ucu.Poo.RoleplayGame;

public class Encounter
{
    private bool end = false;
    private List<ICharacter> heroes = new List<ICharacter>();
    private List<ICharacter> enemies = new List<ICharacter>();

    public Encounter(ICharacter heroe, ICharacter enemy)
    {
        AddCharacterToTeam(heroe, heroes, enemies);
        AddCharacterToTeam(enemy, heroes, enemies);
    }

    public void AddCharacter(ICharacter character)
    {
        AddCharacterToTeam(character, heroes, enemies);
        Console.WriteLine($"\nSe ha agregado a {character.Name} al combate.\n");
    }

    private void AddCharacterToTeam(ICharacter character, List<ICharacter> heroes, List<ICharacter> enemies)
    {
        if (character is EvilDwarf || character is EvilArcher || character is EvilWizard || character is EvilKnight)
        {
            enemies.Add(character);
        }
        else
        {
            heroes.Add(character);
        }
    }

    public void DoEncounter()
    {
        if (heroes.Count == 0 || enemies.Count == 0)
        {
            Console.WriteLine("\nERROR: No hay suficientes héroes/enemigos para el combate.\n");
            return;
        }

        Console.WriteLine("\nINICIO DEL COMBATE:\n");
        while (!end)
        {
            Console.WriteLine("Ataque Enemigo:");
            RealizarAtaque(enemies, heroes, false);
            if (!end)
            {
                Console.WriteLine("Ataque de Héroes:");
                RealizarAtaque(heroes, enemies, true);
            }
        }
    }

    private void RealizarAtaque(List<ICharacter> atacantes, List<ICharacter> defensores, bool esHeroe)
    {
        List<ICharacter> defensoresEliminados = new List<ICharacter>();

        for (int i = 0; i < atacantes.Count && defensores.Count > 0; i++)
        {
            ICharacter atacante = atacantes[i];
            ICharacter defensor = defensores[i % defensores.Count];
            int defensorHealthAntes = defensor.Health;

            defensor.ReceiveAttack(atacante.AttackValue);
            Console.WriteLine($"\t{atacante.Name} atacó a {defensor.Name}.");

            if (defensor.Health == defensorHealthAntes)
            {
                Console.WriteLine($"\tNo logró sacarle vida.");
            }
            Console.WriteLine($"\tLa nueva vida de {defensor.Name} es de {defensor.Health}.\n");

            if (defensor.Health <= 0)
            {
                Console.WriteLine($"\t{defensor.Name} ha muerto.");
                defensoresEliminados.Add(defensor);

                if (esHeroe)
                {
                    atacante.VP += defensor.VP;
                    Console.WriteLine($"\t{atacante.Name} ha adquirido {defensor.VP} puntos de victoria.");
                    Console.WriteLine($"\t{atacante.Name} posee actualmente {atacante.VP} puntos de victoria.\n");

                    if (atacante.VP >= 5)
                    {
                        atacante.Cure();
                        Console.WriteLine($"\t{atacante.Name} se ha curado. Su vida actual es de {atacante.Health}.\n");
                    }
                }
            }
        }

        foreach (var eliminado in defensoresEliminados)
        {
            defensores.Remove(eliminado);
        }

        if (defensores.Count == 0)
        {
            end = true;
            Console.WriteLine($"\nFIN DEL COMBATE: {(esHeroe ? "Ganaron los héroes." : "Ganaron los enemigos.")}");
        }
    }
}
