package com.example.lessons.service;

import com.example.lessons.Repository.UserRepository;
import com.example.lessons.model.User;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class UserService {

    private final UserRepository userRepository;

    public UserService(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    public Optional<User> findUser(Long id){
        return userRepository.findById(id);
    }
    public User saveUser(User user){
        return userRepository.save(user);
    }

}
