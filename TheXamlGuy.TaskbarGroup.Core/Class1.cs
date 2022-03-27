namespace TheXamlGuy.TaskbarGroup.Core
{

    public static class Class1
    {
        public static void Test(Stream s)
        {
            using (FileStream outputFileStream = new(@"C:\Users\Daniel Clark\Pictures\trst.png", FileMode.Create))
            {
                s.CopyTo(outputFileStream);
            }
        }
    }
}
