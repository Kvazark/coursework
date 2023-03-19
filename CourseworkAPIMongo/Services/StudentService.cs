﻿using System.Collections.Generic;
using CourseworkAPIMongo.Models;
using MongoDB.Driver;

namespace CourseworkAPIMongo.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentService(CourseworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _students = database.GetCollection<Student>(settings.StudentsCollectionName);
        }

        public List<Student> Get() =>
            _students.Find(student => true).ToList();

        public Student Get(string id) =>
            _students.Find<Student>(student => student.Id == id).FirstOrDefault();

        public Student Create(Student student)
        {
            _students.InsertOne(student);
            return student;
        }

        public void Update(string id, Student studentIn) =>
            _students.ReplaceOne(student => student.Id == id, studentIn);

        public void Remove(Student studentIn) =>
            _students.DeleteOne(student => student.Id == studentIn.Id);

        public void Remove(string id) => 
            _students.DeleteOne(student => student.Id == id);
    }
}