using System;

namespace Lab3_TranLeHienDuc_CSE422
{
    public class BookComparison
    {
        public static void DemonstrateBookDifferences()
        {
            Console.WriteLine("=== Demonstrating Differences Between Class and Record ===\n");

            // Create two BookClass instances with the same values
            var book1 = new BookClass("123", "The Great Gatsby", "F. Scott Fitzgerald");
            var book2 = new BookClass("123", "The Great Gatsby", "F. Scott Fitzgerald");
            
            // Create two BookRecord instances with the same values
            var record1 = new BookRecord("123", "The Great Gatsby", "F. Scott Fitzgerald");
            var record2 = new BookRecord("123", "The Great Gatsby", "F. Scott Fitzgerald");

            // 1. Demonstrate reference equality vs value equality
            Console.WriteLine("1. Equality Comparison:");
            Console.WriteLine($"BookClass (Reference Equality): book1 == book2 is {book1 == book2}");
            Console.WriteLine($"BookRecord (Value Equality): record1 == record2 is {record1 == record2}");

            // 2. Demonstrate object copying
            Console.WriteLine("\n2. Object Copying:");
            
            // For class - need to manually copy
            var book3 = new BookClass(book1.ISBN, book1.Title, book1.Author);
            book3.Title = "Updated Title";
            
            // For record - can use with expression
            var record3 = record1 with { Title = "Updated Title" };

            Console.WriteLine("Class copying (manual):");
            Console.WriteLine($"Original: {book1}");
            Console.WriteLine($"Modified copy: {book3}");
            
            Console.WriteLine("\nRecord copying (with expression):");
            Console.WriteLine($"Original: {record1}");
            Console.WriteLine($"Modified copy: {record3}");

            // 3. Demonstrate toString behavior
            Console.WriteLine("\n3. ToString() Behavior:");
            Console.WriteLine($"BookClass ToString(): {book1}");
            Console.WriteLine($"BookRecord ToString(): {record1}");

            // 4. Demonstrate property mutation
            Console.WriteLine("\n4. Property Mutation:");
            book1.Title = "Modified Title";
            // record1.Title = "Modified Title"; // This would cause a compilation error if record was immutable
            
            Console.WriteLine($"Modified BookClass: {book1}");
            
            // 5. Create a new record with modified property using with
            var modifiedRecord = record1 with { Title = "Modified Title" };
            Console.WriteLine($"New record with modified title: {modifiedRecord}");
        }
    }
}
