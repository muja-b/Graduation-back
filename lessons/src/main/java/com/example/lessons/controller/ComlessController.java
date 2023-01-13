package com.example.lessons.controller;

import com.example.lessons.Repository.ComlessRepository;
import com.example.lessons.model.Comless;
import com.example.lessons.model.Coms;
import com.example.lessons.model.Less;
import com.example.lessons.model.Lesson;
import com.example.lessons.service.ComlessService;
import jakarta.websocket.server.PathParam;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@Controller
public class ComlessController {
    private final ComlessService comlessService;
    private final ComlessRepository comlessRepository;

    public ComlessController(ComlessService comlessService,
                             ComlessRepository comlessRepository) {
        this.comlessService = comlessService;
        this.comlessRepository = comlessRepository;
    }

    @CrossOrigin
    @GetMapping("/com/{type}")
    public ResponseEntity<Coms> getAllLessons(@PathVariable Integer type){

        List<Comless> lessons=comlessService.getAllLess(type);
        Coms coms=new Coms();
        coms.setLessons(lessons);
        return new ResponseEntity<>(coms, HttpStatus.OK);
    }
    @CrossOrigin
    @GetMapping("/com/lesson/{id}")
    public ResponseEntity<Optional<Comless>> getLesson(@PathVariable Long id){

        Optional<Comless> lesson=comlessService.getLesson(id);
        return new ResponseEntity<>(lesson, HttpStatus.OK);
    }
    @CrossOrigin
    @DeleteMapping("/com/{id}")
    public void deleteLesson(@PathVariable Long id){
        comlessService.deleteComless(id);
    }

    @CrossOrigin
    @PatchMapping("/com/{id}")
    public ResponseEntity<Comless> increaseAttempts(@PathVariable Long id){
        Optional<Comless>  lesson=comlessService.getLesson(id);
        Comless less=lesson.get();
        less.setFinished((less.getFinished()+1));
        less=comlessRepository.save(less);
        return  new ResponseEntity<>(less, HttpStatus.OK);

    }
    @CrossOrigin
    @PutMapping("/com/add")
    public ResponseEntity<Comless> createLesson(@PathParam("name") String name,@PathParam("text") String text
            ,@PathParam("ownerId") Long ownerId,@PathParam("pub")Boolean pub,@PathParam("ownerName") String ownerName){
        Comless less=new Comless(name,text,pub,ownerId,0,ownerName);
        Comless back=comlessService.saveComless(less);
        return new ResponseEntity<>(back,HttpStatus.OK);
    }
}
