package com.example.lessons.service;


import com.example.lessons.Repository.LessonRepository;
import com.example.lessons.model.Lesson;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class LessonService {
    final private LessonRepository lessonRepository;

    public LessonService(LessonRepository lessonRepository) {
        this.lessonRepository = lessonRepository;
    }

    public List<Lesson> getAllLessons(){
        return lessonRepository.findAll();
    }
    public Optional<Lesson> getLesson(Long id){
        return lessonRepository.findById(id);
    }

    public Lesson saveLesson(Lesson lesson){
        return lessonRepository.save(lesson);
    }
}
