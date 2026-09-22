using EventSourcing.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcing.Data;

public partial class StudentEntity : IMaterializable<StudentEntity>
{
    private StudentEntity Apply(StudentRegisteredEvent evt)
    {
        Id = evt.StudentId;
        FullName = evt.FullName;
        Email = evt.Email;
        DateOfBirth = evt.DateOfBirth;
        return this;
    }

    private StudentEntity Apply(StudentEmailUpdatedEvent evt)
    {
        if (Id == evt.StudentId)
        {
            Email = evt.Email;
        }
        return this;
    }

    private StudentEntity Apply(StudentEnrolledEvent evt)
    {
        if (Id == evt.StudentId)
        {
            if (!EnrolledCourses.Contains(evt.CourseName))
            {
                EnrolledCourses.Add(evt.CourseName);
            }
        }
        return this;
    }

    private StudentEntity Apply(StudentDisenrolledEvent evt)
    {
        if (Id == evt.StudentId)
        {
            EnrolledCourses.Remove(evt.CourseName);
        }
        return this;
    }

    public StudentEntity Apply(DomainEvent evt) => evt switch
    {
        StudentRegisteredEvent studentRegisteredEvent => Apply(studentRegisteredEvent),
        StudentEmailUpdatedEvent studentEmailUpdatedEvent => Apply(studentEmailUpdatedEvent),
        StudentEnrolledEvent studentEnrolledEvent => Apply(studentEnrolledEvent),
        StudentDisenrolledEvent studentDisenrolledEvent => Apply(studentDisenrolledEvent),
        _ => this
    };
}
