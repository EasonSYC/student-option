SELECT c.*
FROM ClassSets cs, ClassEnrollments ce, Students s, Courses c
WHERE ce.ClassSetID = cs.ClassSetID
    AND ce.StudentID = s.StudentID
    AND cs.CourseID = c.CourseID
    AND s.StudentID = 1

-- Find all courses taken by student 1 --