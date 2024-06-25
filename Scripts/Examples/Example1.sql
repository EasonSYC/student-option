SELECT s.*
FROM ClassSets cs, ClassEnrollments ce, Students s
WHERE ce.ClassSetId = cs.ClassSetId
    AND ce.StudentId = s.StudentId
    AND cs.ClassSetId = 1

-- Find all students in class 1 --