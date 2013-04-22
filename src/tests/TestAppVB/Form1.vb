

Imports BusinessObjectsVB.Extender
Imports voidsoft.DataBlock
Imports Extender

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        voidsoft.DataBlock.Configuration.ReadConfigurationFromConfigFile()


        Dim a As New Extender.Author

        Dim aa As New AuthorPersistentObject(a)

        Dim ars As Author() = CType(aa.GetTableMetadata(), Author())



        For index As Integer = 1 To ars.Length - 1

            Dim books As Book() = CType(ars(index).GetBook(), Book())


            For index1 As Integer = 1 To books.Length - 1
                Console.WriteLine(books(index1))
                ' Add code to be executed on each iteration.
            Next

            ' Add code to be executed on each iteration.
        Next



        '    Extender.AuthorPersistentObject aa = new AuthorPersistentObject(a);

        '    Author[] ars = (Author[])aa.GetTableMetadata();

        '    foreach (Author var in ars)
        '    {
        'Try
        '        {

        '            Book[] books = (Book[]) var.GetBook();


        '            foreach (Book b in books)
        '            {
        '                Console.WriteLine(b);
        '            }

        '        }
        '        catch (Exception ex)
        '        {
        '            Console.WriteLine(ex);
        '        }

        '    }


    End Sub
End Class
