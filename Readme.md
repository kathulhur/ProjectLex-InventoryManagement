<div id="top"></div>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <!-- <a href="https://github.com/kathulhur/ProjectLex-InventoryManagement">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a> -->

<h3 align="center">Inventory Management System</h3>

  <p align="center">
    An inventory management system that has functionality such as creating, reading, updating, and deleting data records.<br>
    It also contains dashboard that shows an overview of the system, allowing instant view on business insights.<br>
    Also, employees can have privileges which provides confidentiality and security of data.
    <br />
    <a href="https://github.com/kathulhur/ProjectLex-InventoryManagement"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/kathulhur/ProjectLex-InventoryManagement">View Demo</a>
    ·
    <a href="https://github.com/kathulhur/ProjectLex-InventoryManagement/issues">Report Bug</a>
    ·
    <a href="https://github.com/kathulhur/ProjectLex-InventoryManagement/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

[![Product Name Screen Shot][product-screenshot]](https://example.com)


<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

* [Windows Presentation Foundation](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/overview/?view=netdesktop-6.0)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli)
* [Material Design](http://materialdesigninxaml.net/)
* [Live Charts](https://lvcharts.net/)
* [SQL Server Developer](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

### Prerequisites
* Ensure that all the <a href="#built-with">frameworks</a> and <a href="#built-with">libraries</a> are properly installed on visual studio and on your machine


### Database setup
1. Set the database module as the startup project (Check whether the output type is a class library to prevent potential errors)
2. Open the Package Manager Console (Make sure that the Database module is selected as the default project)
3. Perform the following command:
> Before running the command check whether Microsoft.EntityFrameworkCore.Design and Microsoft.EntityFrameworkCore.Tools are installed to the solution
  ```sh
  Add-Migration MigrationName
  ```

  ```sh
  Update-Database
  ```
4. Place the connection string of the database created in the dbconfig.json file located in the database module
5. Create an Admin role in the Role table, setting all the privileges to true
6. Add a single record in the Staff table to be used for initial login

### Installation
> You may want to run in Debug mode first
1. Set the Desktop module as the startup project
2. Set the configuration from Debug to Release
3. Build the solution

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
<!-- ## Usage

Use this space to show useful examples of how a project can be used. Additional screenshots, code examples and demos work well in this space. You may also link to more resources.

_For more examples, please refer to the [Documentation](https://example.com)_

<p align="right">(<a href="#top">back to top</a>)</p> -->



<!-- ROADMAP -->
## Roadmap

- [ ] Fix the product coding system (make it informative)
- [ ] Password Encryption

See the [open issues](https://github.com/kathulhur/ProjectLex-InventoryManagement/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Joseph - josephkarl19@outlook.com

Project Link: [https://github.com/kathulhur/ProjectLex-InventoryManagement](https://github.com/kathulhur/ProjectLex-InventoryManagement)

<p align="right">(<a href="#top">back to top</a>)</p>





<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->

<!-- contributors -->
[contributors-shield]: https://img.shields.io/github/contributors/kathulhur/ProjectLex-InventoryManagement.svg?style=for-the-badge
[contributors-url]: https://github.com/kathulhur/ProjectLex-InventoryManagement/graphs/contributors

<!-- fork -->
[forks-shield]: https://img.shields.io/github/forks/kathulhur/ProjectLex-InventoryManagement.svg?style=for-the-badge
[forks-url]: https://github.com/kathulhur/ProjectLex-InventoryManagement/network/members

<!-- stars -->
[stars-shield]: https://img.shields.io/github/stars/kathulhur/ProjectLex-InventoryManagement.svg?style=for-the-badge
[stars-url]: https://github.com/kathulhur/ProjectLex-InventoryManagement/stargazers

<!-- issues -->
[issues-shield]: https://img.shields.io/github/issues/kathulhur/ProjectLex-InventoryManagement.svg?style=for-the-badge
[issues-url]: https://github.com/kathulhur/ProjectLex-InventoryManagement/issues

<!-- license -->
[license-shield]: https://img.shields.io/github/license/kathulhur/ProjectLex-InventoryManagement.svg?style=for-the-badge
[license-url]: https://github.com/kathulhur/ProjectLex-InventoryManagement/blob/master/LICENSE.txt

<!-- LinkedIn -->
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/joseph-karl-crisostomo-aa009021b/
[product-screenshot]: images/screenshot.png
