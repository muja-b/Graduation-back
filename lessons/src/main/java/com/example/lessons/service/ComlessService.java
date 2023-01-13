package com.example.lessons.service;

import com.example.lessons.Repository.ComlessRepository;
import com.example.lessons.model.Comless;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class ComlessService {
    private final ComlessRepository comlessRepository;

    public ComlessService(ComlessRepository comlessRepository) {
        this.comlessRepository = comlessRepository;
    }

    public Optional<Comless> getLesson(Long id){
        return comlessRepository.findById(id);
    }

    public List<Comless> getAllLess(Integer type){
        List<Comless> lessons;
        if(type==1){
            lessons=comlessRepository.getAllByPubOrderByIdAsc();
        }
        else if(type==2){
            lessons=comlessRepository.getAllByPubOrderByIdDesc();
        }
        else {
            lessons=comlessRepository.getAllByPubOrderByFinishedDesc();
        }
        return lessons;
    }

    public Comless saveComless(Comless less){
        return comlessRepository.save(less);
    }

    public void deleteComless(Long id){
        Optional<Comless> less=comlessRepository.findById(id);
        Comless comless=less.get();
        comlessRepository.delete(comless);
    }
}
