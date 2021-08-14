pipeline {
    agent any
    environment {
        dotnet = 'path\\to\\dotnet.exe'
    }
    stages {
    	stage('Restore Packages') {
            steps {
                bat "dotnet restore SWP490_G9_PE/TnRSS.sln"
            }
    	}
        stage('Clean') {
            steps {
            bat "dotnet clean SWP490_G9_PE/TnRSS.sln"
            }
        }
        stage('Build') {
            steps {
                bat 'dotnet build SWP490_G9_PE/TnRSS.sln --configuration Release'
            }
        }  
        // stage('Test Dotnet ef') {
        //     steps {
        //         bat 'dotnet ef --version'
        //     }
        // }  
        // stage('Update database') {
        //     steps {
        //         bat ' dotnet ef database -s SWP490_G9_PE/TnR_SS.API update'
        //     }
        // }
        stage('Publish') {
            steps {
                bat 'dotnet publish SWP490_G9_PE/TnRSS.sln -p:PublishProfile=FolderProfile'
            }
        }    
    }
}
