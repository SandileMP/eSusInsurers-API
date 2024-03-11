# eSusfarm Crop Insurance Management with Etherisc Blockchain Integration and AIC

1. eSusfarm will receive crop monitoring index insurance details data from AIC through an API, ensuring seamless integration of information.
2. Leveraging the Etherisc blockchain API, eSusfarm will establish communication with the Etherisc blockchain (https://github.com/etherisc/esusfarm), facilitating secure and transparent data transactions.
3. Upon receiving the data, eSusfarm will post it onto the Etherisc blockchain, ensuring the integrity and immutability of the information.
4. All vital details regarding insurance policies, claim requests, and updates will be securely stored on the Etherisc blockchain, enhancing data security and accessibility.
5. eSusfarm's user-friendly web application will serve as an interface for AIC, empowering the insurance company to efficiently register insurers and manage crop insurance information.
6. Farmers will initiate insurance claims and requests for crop coverage through the eSusfarm platform, utilizing the convenient USSD interface for accessibility and ease of use.
7. AIC will exercise its authority to review, approve, or reject insurance claims and requests through the intuitive eSusfarm web platform, streamlining the decision-making process.
8. Every action undertaken by AIC concerning insurance claims and requests will be meticulously recorded on the Etherisc blockchain, ensuring transparency and accountability.
9. eSusfarm will continuously communicate with the Etherisc blockchain via its dedicated API, enabling real-time updates and synchronization of data for all stakeholders involved.
10. Through this robust and interconnected ecosystem, eSusfarm, AIC, and farmers will collectively benefit from enhanced efficiency, transparency, and security in managing crop insurance processes.

# Backend Architecture
![eSusInsurance-Backend Architecture drawio (2)](https://github.com/eSusFarm/eSusInsurers-API/assets/88090641/f8ca4c07-3d6a-485f-a94e-40211a70fec4)

# ASP.NET Core Features
* Creating an API backend service
* Isolating business logic and Controller code
* Using a base Repository for business logic
* Separating code into separate projects
* Custom User Authentication
* CORS Support
* Using ASP.NET Core as a backend to an Angular 17.0 front end

Version supported:

* .NET Core 8.0
* 8.0 SDK or later
* Visual Studio 2022 or later

# .NET Core API Configuration
You should just be able to clone this repo as is on either Windows or Mac (and probably Linux) and do:
```
cd <Base Solution Folder>
dotnet restore
cd .\eSusFarm\eSusInsurers-API
dotnet run
```


