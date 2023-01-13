package com.example.lessons.controller;

import com.example.lessons.Repository.UserRepository;
import com.example.lessons.model.User;
import jakarta.websocket.server.PathParam;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.Optional;

@Controller
public class UserController {
    private final UserRepository userRepository;

    public UserController(UserRepository userRepository) {
        this.userRepository = userRepository;
    }
    @CrossOrigin
    @GetMapping("/user/{id}")
    public ResponseEntity<Optional<User>> getUser(@PathVariable Long id){
        Optional<User> user =userRepository.findById(id);
        return new ResponseEntity<>(user, HttpStatus.OK);
    }

    @CrossOrigin
    @PostMapping("/user/{id}")
    public ResponseEntity<Optional<User>> updateStats(@PathVariable Long id, @PathParam("wpm") Integer wpm, @PathParam("progress") Integer progress){
        Optional<User> user =userRepository.findById(id);
        User user1=user.get();
        if(wpm>user1.getHighestWPM()){
            user1.setHighestWPM(wpm);
        }
        if(progress>user1.getProgress()){
            user1.setProgress(progress);
        }
        userRepository.save(user1);
        return new ResponseEntity<>(user, HttpStatus.OK);
    }

    @CrossOrigin
    @PostMapping("/user/{id}/updateDescription")
    public ResponseEntity<Optional<User>> updateDes(@PathVariable Long id, @PathParam("Description") String  description){
        Optional<User> user =userRepository.findById(id);
        User user1=user.get();
        user1.setDescription(description);
        userRepository.save(user1);
        return new ResponseEntity<>(user, HttpStatus.OK);
    }

    @CrossOrigin
    @PutMapping("/user/{id}/updateProfilePic")
    public ResponseEntity<Optional<User>> updatePic(@PathVariable Long id, @PathParam("profilePic") String  profilePic){
        Optional<User> user =userRepository.findById(id);
        User user1=user.get();
        user1.setProfilePicture(profilePic);
        userRepository.save(user1);
        return new ResponseEntity<>(user, HttpStatus.OK);
    }

    @CrossOrigin
    @PostMapping("/user/")
    public User saveUser(@PathParam("id") Long id,@PathParam("email") String email,@PathParam("firstName") String firstName,@PathParam("lastName") String lastName){
        User user=new User(firstName,lastName,email,1,0,"https://mdbootstrap.com/img/Photos/new-templates/bootstrap-chat/ava3.webp","");
        user.setId(id);
        userRepository.save(user);
        return user;
    }
}
