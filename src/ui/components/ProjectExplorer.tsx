import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { FileTreeNode, ProjectMetrics } from '../types';

export const ProjectExplorer: React.FC<{ projectPath: string }> = ({ projectPath }) => {
    const [fileTree, setFileTree] = useState<FileTreeNode | null>(null);
    const [metrics, setMetrics] = useState<ProjectMetrics | null>(null);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        loadProjectData();
    }, [projectPath]);

    const loadProjectData = async () => {
        try {
            const [treeData, metricsData] = await Promise.all([
                axios.get<FileTreeNode>(`/api/filetree?projectPath=${projectPath}`),
                axios.get<ProjectMetrics>(`/api/metrics?projectPath=${projectPath}`)
            ]);
            setFileTree(treeData.data);
            setMetrics(metricsData.data);
            setError(null);
        } catch (err) {
            setError('Failed to load project data. Please try again.');
            console.error('Error loading project data:', err);
        }
    };

    const renderFileTree = (node: FileTreeNode) => (
        <ul key={node.path}>
            <li>
                {node.type === 'file' ? '📄 ' : '📁 '}
                {node.name}
                {node.children && node.children.map(child => renderFileTree(child))}
            </li>
        </ul>
    );

    if (error) {
        return <div className="error">{error}</div>;
    }

    return (
        <div className="project-explorer">
            <div className="metrics-panel">
                {metrics && (
                    <>
                        <h3>Project Metrics</h3>
                        <div>Total Files: {metrics.totalFiles}</div>
                        <div>Total Lines: {metrics.totalLinesOfCode}</div>
                        <div>Avg Complexity: {metrics.averageCyclomaticComplexity.toFixed(2)}</div>
                        <div>Max Complexity: {metrics.maxCyclomaticComplexity} ({metrics.fileWithMaxComplexity})</div>
                    </>
                )}
            </div>
            <div className="file-tree">
                <h3>File Structure</h3>
                {fileTree && renderFileTree(fileTree)}
            </div>
        </div>
    );
};
