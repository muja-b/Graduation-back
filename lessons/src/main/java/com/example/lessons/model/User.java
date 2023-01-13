package com.example.lessons.model;


import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import lombok.*;

@Entity
@Setter
@Getter
@RequiredArgsConstructor
@NoArgsConstructor
public class User {
    @Id
    private Long id;

    @NonNull
    String firstName;

    @NonNull
    String LastName;

    @NonNull
    String email;

    @NonNull
    Integer progress;

    @NonNull
    Integer HighestWPM;

    @NonNull
    String profilePicture;

    @NonNull
    String description;
}
