using EventSourcing.Data;
using EventSourcing.Events;

namespace EventSourcing
{
    internal class Program
    {
        const string StudentId = "96A40BE5-7C7D-4385-8493-975873E959BE";

        static readonly InMemoryEventStore<StudentEntity> store = new();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var studentId = Guid.Parse(StudentId);

            // 1. Events persistieren
            PersistSomeStudentEvents(studentId);

            // 2. Student aus EventSourcing materialisieren
            MaterializeEntity(studentId);

            // 3. Student aus projezierter View
            var studentFromView = store.GetEntityProjection(studentId);
            Console.WriteLine($"Student {studentFromView.FullName} aus projezierter View");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void MaterializeEntity(Guid studentId)
        {
            var student = store.GetEntity(studentId);
            Console.WriteLine($"{student.FullName} wurde aus EventSourcing vollstaendig materialisiert.");
        }

        private static void PersistSomeStudentEvents(Guid studentId)
        {
            var registredEvent = new StudentRegisteredEvent
            {
                StudentId = studentId,
                FullName = "Bugs Bunny",
                Email = "bugs@bunny.com",
                DateOfBirth = new DateTime(1940, 7, 27)
            };
            store.Append(registredEvent);
            Console.WriteLine($"Student {registredEvent.FullName} wurde registriert.");

            var enrolledEvent = new StudentEnrolledEvent
            {
                StudentId = studentId,
                CourseName = "Wie lässt man eine AI Pizza backen"
            };
            store.Append(enrolledEvent);
            Console.WriteLine($"Student {registredEvent.FullName} hat sich bei {enrolledEvent.CourseName} angemeldet.");

            var emailUpdatedEvent = new StudentEmailUpdatedEvent
            {
                StudentId = studentId,
                Email = "whats.up@doc.de"
            };
            store.Append(emailUpdatedEvent);
            Console.WriteLine($"Student {registredEvent.FullName} hat seine E-Mail Adresse geändert.");
        }
    }
}
