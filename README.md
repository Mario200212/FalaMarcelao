# FalaMarcelao

The FalaMarcelão App consists of an intuitive interface that takes a sentence or word in Brazilian Portuguese and converts it into images of mouths representing the sounds of each phoneme. The app also offers text-to-speech functionality with speed control, allowing users to adjust the pronunciation speed as needed. For this reason, FalaMarcelão is ideal for patients with aphasia or apraxia who have the ability to write words or short sentences. See below:
![https://example.com/image.jpg](https://store-images.s-microsoft.com/image/apps.50091.13721141960411444.d88f071a-c707-494b-b7e6-7234ec6b4ec7.bcc8713d-193a-4ccc-b5ee-bc86d2ac28f4)

The FalaMarcelão app is now available on the Microsoft Store. <a href="https://apps.microsoft.com/detail/9nl10c8s34fp?hl=pt-br&gl=BR" target="_blank">Click here</a> to download.

## Table of Contents

- [Installation](#installation)
- [Project Background](#project-background)
- [Mouth Image Database](#mouth-image-database)
- [Contributing to the Project](#contributing-to-the-project)
  
## Installation
- Clone the repository:
    ```bash
    git clone https://github.com/Mario200212/FalaMarcelao.git
    ```
## Project Background
- Language Used: C#
- Framework Used: .NET MAUI
### Development Notes: 
In general, for the development of the application, only two files from this repository were used:
- MainPage.xaml: Used to create the visual interface of the application
- MainPage.xaml.cs: Used for building the entire backend of the application (Text-to-Speech, Text-to-Mouths, ...)

## Mouth Image Database
The project's image files are available in the 'Resources/Images' folder. See some examples below:
<div style="display: flex; justify-content: space-between;">
    <img src="Resources/Images/a.png" alt="Descrição da Imagem 1" style="width: 30%; height: auto;">
    <img src="Resources/Images/b.png" alt="Descrição da Imagem 2" style="width: 30%; height: auto;">
    <img src="Resources/Images/m.png" alt="Descrição da Imagem 3" style="width: 30%; height: auto;">
</div>


## Contributing to the Project

There are several ways you can contribute to this project:

### 1. Expanding the App to Other Languages

#### 1.1 Mouth Images
- Provide photos of mouths representing all the phonemes of a language you know.
- If photos for the phonemes of a given language already exist, you can use a tool like Canva to format the images as follows:
  {photo_here}

#### 1.2 Programming
- If you are familiar with C# and the .NET MAUI framework, you can implement the logic to convert Text to Mouths (see `MainPage.xaml.cs`).
- **Note 1:** If you are implementing the app in a language other than Brazilian Portuguese, please make a copy of the initial repository and submit it as a new branch. I will handle merging your language implementation with the existing languages into a single app.
- **Note 2:** We will always give proper credit for all contributions made by our contributors.
- You can also enhance the algorithm used for Text to Mouth conversion and implement any new ideas that could improve the current app.

Thank you for your interest in contributing to the project!









