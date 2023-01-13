package com.example.lessons.Repository;

import com.example.lessons.model.Comless;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface ComlessRepository extends JpaRepository<Comless, Long> {

    @Query("SELECT c FROM Comless c WHERE c.pub = true ORDER BY c.id ASC")
    public List<Comless> getAllByPubOrderByIdAsc();

    @Query("SELECT c FROM Comless c WHERE c.pub = true ORDER BY c.id DESC ")
    public List<Comless> getAllByPubOrderByIdDesc();


    @Query("SELECT c FROM Comless c WHERE c.pub = true ORDER BY c.finished DESC ")
    public List<Comless> getAllByPubOrderByFinishedDesc();
}
