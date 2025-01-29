const metricsCache = new Map<string, ProjectMetrics>();
import axios, { AxiosError } from 'axios';
import { FileTreeNode, ProjectMetrics, AnalysisResult } from '../types';

const API_BASE_URL = 'http://localhost:5000/api';

export const projectService = {
    async getFileTree(projectPath: string): Promise<FileTreeNode> {
        try {
            const response = await axios.get(`${API_BASE_URL}/filetree`, { params: { projectPath } });
            return response.data;
        } catch (error) {
            handleApiError(error as AxiosError);
            throw error;
        }
    },

    async getMetrics(projectPath: string): Promise<ProjectMetrics> {
        if (metricsCache.has(projectPath)) {
            return metricsCache.get(projectPath)!;
        }
        try {
            const response = await axios.get(`${API_BASE_URL}/metrics`, { params: { projectPath } });
            const metrics = response.data;
            metricsCache.set(projectPath, metrics);
            return metrics;
        } catch (error) {
            handleApiError(error as AxiosError);
            throw error;
        }
    },

    async analyzeProject(projectPath: string): Promise<AnalysisResult> {
        try {
            const response = await axios.post(`${API_BASE_URL}/analyze`, { projectPath });
            return response.data;
        } catch (error) {
            handleApiError(error as AxiosError);
            throw error;
        }
    },

    async getProjectDetails(projectPath: string): Promise<ProjectDetails> {
        try {
            const response = await axios.get(`${API_BASE_URL}/projectdetails`, { params: { projectPath } });
            return response.data;
        } catch (error) {
            handleApiError(error as AxiosError);
            throw error;
        }
    }
};

function handleApiError(error: AxiosError) {
    if (error.response) {
        console.error('API Error:', error.response.data);
        console.error('Status:', error.response.status);
    } else if (error.request) {
        console.error('No response received:', error.request);
    } else {
        console.error('Error setting up request:', error.message);
    }
}

// Types remain the same as before
