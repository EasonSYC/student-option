SELECT c.*
FROM ClassSets cs, ClassEnrollments ce, Students s, Courses c
WHERE ce.ClassSetId = cs.ClassSetId
    AND ce.StudentId = s.StudentId
    AND cs.CourseId = c.CourseId
    AND s.StudentId = 1

-- Find all courses taken by student 1 --