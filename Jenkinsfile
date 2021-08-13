pipeline {
    agent any
    environment {
        dotnet = 'path\\to\\dotnet.exe'
    }
    stages {
    	stage('Restore Packages') {
            steps {
                bat "dotnet restore"
            }
    	}
        stage('Clean') {
            steps {
            bat "dotnet clean"
            }
        }
        stage('Test') {
            steps {
                bat 'dotnet test'
            }
        }
        stage('Build') {
            steps {
                bat 'dotnet build'
            }
        }       
    }
}
