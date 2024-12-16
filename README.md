# tu-multicloud-reference
 
This project is a reference project for supporting deployment to AWS via pre-existing Docker mechanism, as well as Azure via the pre-existing Service Fabric mechanism.

To run the AWS version, either configure the CloudWatch settings, or comment them out, set it to the Startup Project and run. You can run without Docker, but as it's designed to prove it runs via that path, it's preferable to run it as a container.

To run the Azure version, set the .Service project to be the Startup Project and run it.