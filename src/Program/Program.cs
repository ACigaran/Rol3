using System;

namespace Ucu.Poo.RoleplayGame.Program;

class Program
{
    static void Main(string[] args)
    {
        //Crear personajes
        Dwarf gimli = new Dwarf("cuete");
        Wizard patolucas = new Wizard("feijoada");
        EvilKnight elmalo = new EvilKnight("enemie");
        EvilArcher masmalo = new EvilArcher("enemie2");
        
        //Agregarles items
        gimli.AddItem(new Sword());
        SpellsBook book1 = new SpellsBook();
        book1.AddSpell(new SpellOne());
        patolucas.AddItem(book1);
        elmalo.AddItem(new Bow());
        masmalo.AddItem(new Shield());
        
        //Conocer valores de personajes
        Console.WriteLine($"\n({gimli.Name}) vida: {gimli.Health}, defensa: {gimli.DefenseValue}, ataque: {gimli.AttackValue}");
        Console.WriteLine($"({patolucas.Name}) vida: {patolucas.Health}, defensa: {patolucas.DefenseValue}, ataque: {patolucas.AttackValue}");
        Console.WriteLine($"({elmalo.Name}) vida: {elmalo.Health}, defensa: {elmalo.DefenseValue}, ataque: {elmalo.AttackValue}");
        Console.WriteLine($"({masmalo.Name}) vida: {masmalo.Health}, defensa: {masmalo.DefenseValue}, ataque: {masmalo.AttackValue}\n");
        
        //Crear combate y ejecutarlo
        Encounter firstEncounter = new Encounter(gimli, elmalo);
        firstEncounter.AddCharacter(patolucas);
        firstEncounter.AddCharacter(masmalo);
        firstEncounter.DoEncounter();
    }
}
