package com.example.lessons.controller;

import com.example.lessons.model.Less;
import com.example.lessons.model.Lesson;
import com.example.lessons.service.LessonService;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;

import java.util.List;
import java.util.Optional;

@Controller
public class LessonController {
    final private LessonService lessonService;

    public LessonController(LessonService lessonService) {
        this.lessonService = lessonService;
    }
    @CrossOrigin
    @GetMapping("/")
    public ResponseEntity<Less> getAllLessons(){

        List<Lesson> lessons=lessonService.getAllLessons();
        Less less=new Less();
        less.setLessons(lessons);
        return new ResponseEntity<>(less, HttpStatus.OK);
    }
    @CrossOrigin
    @GetMapping("/{id}")
    public ResponseEntity<Optional<Lesson>> getLesson(@PathVariable Long id){
        Optional<Lesson> lesson=lessonService.getLesson(id);
        return new ResponseEntity<>(lesson, HttpStatus.OK);
    }


}
